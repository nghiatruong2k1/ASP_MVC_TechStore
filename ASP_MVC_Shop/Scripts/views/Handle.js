
const Loading = (function () {
    const loadings = document.createElement("div");
    loadings.classList.add("page-loading-container");
    document.body.appendChild(loadings);

    const loading = document.createElement("button");
    loading.classList.add("page-loading", "btn");
    loadings.appendChild(loading);

    const icon = document.createElement("span");
    icon.classList.add("fas", "fa-sync-alt", "page-loading-icon");
    loading.appendChild(icon);

    let showCount = 0;
    function handleShow() {
        loading.dataset.count = showCount;
        if (showCount > 0) {
            loading.classList.add("show");
        } else {
            loading.classList.remove("show");
        }
    }
    handleShow();
    return {
        show: function () {
            showCount++;
            handleShow();
        }, remove: function () {
            if (showCount > 0) {
                showCount--;
                handleShow();
            }
        }
    }
}());



const Toast = (function () {
    const container = document.createElement("div");
    container.classList.add("toast-container");
    document.body.appendChild(container);

    const toasts = document.createElement("div");
    toasts.classList.add("toast-content")
    container.appendChild(toasts);

    const TIME_CLEAR = 250;
    const TIME_OUT = 20000;
    function setStyle(element, type) {
        switch (type) {
            case 'close': {
                element.style.color = "#f00";
                break;
            }
            case 'success': {
                element.style.color = "var(--success)";
                break;
            }
            case 'error': {
                element.style.color = "var(--danger)";
                break;
            }
            case 'warning': {
                element.style.color = "var(--warning)";
                break;
            }
            default: {
                element.style.color = "var(--info)";
                break;
            }
        }
    }
    function newTitle({ type, title }) {
        const titleElement = document.createElement("strong");
        titleElement.classList.add("flex-fill","toast-title");
        titleElement.innerText = Boolean(title) && title || "Thông báo";
        setStyle(titleElement, type);

        return titleElement;
    }
    function newIcon(type) {
        const iconElement = document.createElement("span");
        iconElement.classList.add("icon");
        switch (type) {
            case 'close': {
                iconElement.classList.add("fa", "fa-times");
                break;
            }
            case 'success': {
                iconElement.classList.add("fas", "fa-check", "mx-2");
                break;
            }
            case 'error': {
                iconElement.classList.add("fas", "fa-skull-crossbones", "mx-2");
                break;
            }
            case 'warning': {
                iconElement.classList.add("fas", "fa-exclamation-triangle", "mx-2");
                break;
            }
            default: {
                iconElement.classList.add("fas", "fa-bell", "mx-2");
                break;
            }
        }
        setStyle(iconElement, type);
        return iconElement;
    }
    function newToast({ type, title, message }) {
        const toast = document.createElement("div");
        toast.classList.add("toast");
        toast.style.transitionDuration = TIME_CLEAR / 1000 + "s";
        toast.setAttribute("role", "alert");
        toast.setAttribute("aria-live", "assertive");
        toast.setAttribute("aria-atomic", "true");

        const head = document.createElement("div");
        head.classList.add("toast-header");

        head.appendChild(newIcon(type));
        head.appendChild(newTitle({ type, title }));

        const time = document.createElement("small");

        const closeButton = document.createElement("button");
        closeButton.setAttribute("type", "button");
        closeButton.setAttribute("data-bs-dismiss", "toast");
        closeButton.setAttribute("aria-label", "Close");
        closeButton.classList.add("btn", "btn-close", "p-0", "d-flex", "align-items-center", "justify-content-center");
        closeButton.style.color = "#f00";
        closeButton.style.width = "1.6em";
        closeButton.style.height = "1.6em";
        closeButton.appendChild(newIcon("close"));

        closeButton.addEventListener('click', function (e) {
            if (toast.classList.contains("show")) {
                toast.classList.remove("show");
                setTimeout(function () {
                    toasts.removeChild(toast);
                }, TIME_CLEAR)
            }

        })

        setTimeout(function () {
            closeButton.click();
        }, TIME_OUT)

        const body = document.createElement("div");
        body.classList.add("toast-body");
        const content = document.createElement("p");
        content.innerHTML = message;

        toast.appendChild(head);
        head.appendChild(time);
        head.appendChild(closeButton);

        toast.appendChild(body);
        body.appendChild(content);

        toasts.appendChild(toast);
        toast.classList.add("show", "fade");
    }
    return function (type, title, message) {
        newToast({ type, title, message })
    }
}());

function startJax(element) {
    Loading.show();
    if (element) {
        element.classList.add("disabled");
    }
}
function stopJax(element) {
    setTimeout(function () {
        if (element) {
            element.classList.remove("disabled");
        }
        Loading.remove();
    }, 1000)
}

function formatBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = (e) => resolve(e.target.result);
        reader.onerror = error => reject(error);
    });
}
function formatNumber(number, n, x) {
    let re = '\\d(?=(\\d{' + (n || 3) + '})+' + (x > 0 ? '\\.' : '$') + ')';
    return number.toFixed(Math.max(0, ~~x)).replace(new RegExp(re, 'g'), '$&,');
}
const parser = new DOMParser();

function handleRender({view,target}) {
    var doc = parser.parseFromString(view, 'text/html');
    if (Array.isArray(target)) {
        target.forEach(function (tar) {
            const content = doc.querySelector(tar);
            console.log(tar, content);
            if (content) {
                $(tar).html(content.innerHTML);
            }
        })
    } else if (typeof (target) == 'string') {
        $(target).html(doc.querySelector(target).innerHTML);
    } else {
        document.documentElement.innerHTML = doc.documentElement.innerHTML;
        
    }
}
function handleLoad({ href, element, target }) {
    startJax(element);
    window.history.pushState({}, document.title, href);
    document.documentElement.scrollTop = 0;
    $.ajax({
            type: "GET",
            url: href,
            dataType: "html",
            success: function (view) {
                handleRender({ view, target })
                stopJax(element)
            }, error: function () {
                stopJax(element)
            }

    });
 }