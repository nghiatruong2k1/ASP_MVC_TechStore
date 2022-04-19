(function () {
    window.addEventListener("popstate", function (event) {
        event.preventDefault();
    });
}());
(function () {
    const header = document.querySelector(".header-main");
    if (header) {
        handleSroll();
        document.addEventListener("scroll", handleSroll)

        function handleSroll() {
            let scrollTop = document.documentElement.scrollTop;
            if (scrollTop > 0) {
                header.classList.add("fixed");
            } else {
                header.classList.remove("fixed");
            }
        }
    }
}());
(function () {
    const cardDeals = document.querySelectorAll(".card-deal");
    Array.from(cardDeals).forEach(function (cardDeal) {
        const timeEnd = new Date(cardDeal.querySelector(".timer").dataset.end);
        let interval;

        const timeD = cardDeal.querySelector(".num.d");
        const timeH = cardDeal.querySelector(".num.h");
        const timeM = cardDeal.querySelector(".num.m");
        const timeS = cardDeal.querySelector(".num.s");

        handleTime(timeEnd, timeS, timeM, timeH, timeD);

        interval = setInterval(function () {
            if (handleTime(timeEnd, timeS, timeM, timeH, timeD)) {
                clearInterval(interval);
            };
        }, 1000)
    })

    function handleTime(timeEnd, timeS, timeM, timeH, timeD) {
        let time = (timeEnd.getTime() - (new Date).getTime()) / 1000;
        if (time >= 0) {
            loadTime(timeS, time % 60);
            time = Math.round(time / 60);
            loadTime(timeM, time % 60);
            time = Math.round(time / 60);
            loadTime(timeH, time % 24);
            time = Math.round(time / 24);
            loadTime(timeD, time);
            return false;
        } else {
            return true;
        }
    }

    function loadTime(element, time) {
        let newTime = Math.round(Number(time));
        if (newTime < 10) {
            element.innerText = "0" + newTime;
        } else {
            element.innerText = newTime;
        }
    }
}());

/*
 (function () {
    const links = document.querySelectorAll(".navbar-container a");
    Array.from(links).forEach(function (link, index) {
        if (link.href.toLowerCase() == window.location.href.toLowerCase()) {
            link.classList.add("active");
        }
    })
}());
 */



;(function () {
    const formSearchs = document.querySelectorAll(".form-search");
    Array.from(formSearchs).forEach(function (formSearch, index) {
        const selectType = formSearch.querySelector(".form-search-type");
        setAction(formSearch, selectType);
        selectType.addEventListener("input", function (e) {
            setAction(formSearch, selectType);
        })
    })
    function setAction(form, select) {
        form.action = select.value;
    }
}());


(function () {
    const form = document.querySelector("#form-content");
    if (form) {
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            startJax(form);
            const inputs = Array.from(event.target).reduce(function (result, element) {
                if (Boolean(element.name)) {
                    switch (element.type) {
                        case "checkbox":
                            result[element.name] = element.checked;
                            break;
                        default:
                            result[element.name] = element.value;
                            break;
                    }
                }
                return result;
            }, {})
            console.log(inputs)
            $.ajax({
                type: form.method,
                url: form.action,
                data: JSON.stringify(inputs),
                dataType: "html",
                success: function (view) {
                    console.log(view)
                    handleRender({
                        view, target: ["#form-content", "#toast-render"]
                    })
                    stopJax(form)
                },
                error: function (e) {
                    console.log(e.responseText)
                    stopJax(form)
                    Toast("error", "", "Có lỗi xảy ra!");
                }
            });
        })
    }
}());