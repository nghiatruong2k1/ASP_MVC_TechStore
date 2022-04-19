(function () {
    const links = document.querySelectorAll(".main-sidebar .nav-link");
    Array.from(links).forEach(function (link, index) {
        const urlLink = link.href.toLowerCase();
        const urlCurrent = window.location.href.toLowerCase().trimEnd();
        if (urlCurrent == urlLink) {
            link.classList.add("active");
        }
    })
}());
function convertToAlias(str) {
    let newStr = str
        .toLowerCase()
        .replace(/\s/g, "-")
        .replace(/[àáảãạâấầẩẫậăắằẳẵặ]/g, "a")
        .replace(/[èéẻẽẹêếềểễệ]/g, "e")
        .replace(/[íìỉĩị]/g, "i")
        .replace(/[óòỏõọôốồổỗộơớờởỡợ]/g, "o")
        .replace(/[úùủũụưứừửữự]/g, "u")
        .replace(/[ýỳỷỹỵ]/g, "y")
        .replace(/[đ]/g, "d")
    return newStr;
};
async function format64(file,viewTarget,viewSrc) {
    if (file) {
        return await formatBase64(file)
            .then(function (result) {
                viewTarget.src = result;
                viewSrc.value = result
            })
    } else {
        return null;
    }
}

function addEventUpload(event) {
    const form = event.target.closest("form");
    const iUpload = form.querySelector("#image-upload");
    const iUrl = form.querySelector("#image-url");
    const iView = form.querySelector("#image-view");

    console.log({ form, iUpload, iView, iUrl})

    if (iUpload.files.length > 0) {
        format64(iUpload.files[0],iView,iUrl);   
    }
}
function addEventName(event) {
    const nameInput = event.target.closest("#name");
    if (nameInput) {
        const form = event.target.closest("form");
        if (form) {
            const aliasInput = form.querySelector("#alias");
            if (aliasInput) {
                aliasInput.value = convertToAlias(nameInput.value);
            }
        }
    }
}
(function () {
    document.addEventListener("input", function (event) {
        console.log(event)
        if (event.target.closest("#name")) {
            addEventName(event);
        } else if(event.target.closest("#image-upload")){
            addEventUpload(event)
        }
    })
    document.addEventListener("change", function (event) {
        const checkInput = event.target.closest("[type='checkbox']");
        if (checkInput) {
            element.value = element.checked;
        }
    })
}());


(function () {
    document.addEventListener("click", function (event) {
        const btns = event.target.closest(".btn-options");
        if (btns) {
            let isRemove = false;
            const btn = event.target.closest(".btn");
            console.log(btn)
            if (btn) {
                if (btn.classList.contains("btn-trash")) {
                    isRemove = true
                } else if (btn.classList.contains("btn-delete")) {
                    isRemove = confirm("Bạn có muốn xóa vĩnh viễn!");
                }
            } else {
                return false
            }
            if (isRemove) {
                startJax(btn);
                $.ajax({
                    type: "POST",
                    url: btn.dataset.action,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function ({ data, toast }) {
                        Toast(toast.type, toast.title, toast.message);
                        $("#content").load(`${document.URL} #content`, {}, function (event) {
                            console.log(event);
                        })
                        stopJax(btn);
                    },
                    error: function (e) {
                        Toast("error", "", "Có lỗi xảy ra!");
                        stopJax(btn);
                    }
                });
            } else {
                stopJax(btn);
            }

        }
    })
}());
function handleSubmit() {
    document.addEventListener("submit", function (event) {
        if (event.target.closest(".form-edit")) {
            const form = event.target.closest(".form-edit");
            event.preventDefault();
            startJax(form);
            const inputs = Array.from(event.target).reduce(function (result, element) {
                if (Boolean(element.name)) {
                    if (element.name == "imageUrl") {
                        result.imageUrl = element.value;
                    } else {
                        switch (element.type) {
                            case "checkbox":
                                result.product[element.name] = element.checked;
                                break;
                            default:
                                result.product[element.name] = element.value;
                                break;
                        }
                    }
                }
                console.log(result)
                return result;
            }, { product: {}, imageUrl: "" })
            $.ajax({
                type: form.method,
                url: form.dataset.action,
                data: JSON.stringify(inputs),
                dataType: "html",
                success: function (view) {
                    handleRender({
                        view, target: ["#content", "#toast-render"]
                    })
                    stopJax(form)
                },
                error: function (e) {
                    stopJax(form)
                    Toast("error", "", "Có lỗi xảy ra!");
                }
            });
        }
    })
};

