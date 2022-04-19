(function () {
    document.addEventListener("click", function (event) {
        const target = event.target.closest(".ajax-page-links");
        if (target) {
            const btn = event.target.closest("a");
            if (btn) {
                handleLoad({
                    href: btn.href+"&isPartical=true",
                    target: target.dataset.render,
                    element: target
                })
            }
            event.preventDefault();
        }
    })
}());
(function () {
    document.addEventListener("click", function (event) {
        const target = event.target.closest(".ajax-view-links");
        if (target) {
            const btn = event.target.closest("button");
            if (btn) {
                handleLoad({
                    href: btn.dataset.href,
                    target: target.dataset.render,
                    element: target
                })
            }
            event.preventDefault();
        }
    })
}());
    