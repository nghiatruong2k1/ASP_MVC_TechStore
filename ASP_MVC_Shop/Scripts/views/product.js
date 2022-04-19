(function () {
    const cartCounts = document.querySelectorAll(".cart-count");
    function loadCount(result) {
        const count = result.reduce(function (out, { quantity }) {
            return out + quantity;
        }, 0);
        Array.from(cartCounts).forEach(function (cartcount) {
            cartcount.innerText = count;
        })
    };

    $.ajax({
        type: "GET",
        url: '/cart/GetData',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            loadCount(result);
        },error: function (e) {
            console.log(e)
        }
    });
    document.addEventListener("click", function (event) {
        const addToCardBtn = event.target.closest(".add-to-cart-btn");
        if (addToCardBtn) {
            const product = event.target.closest(".product");
            const quantityInput = product.querySelector(".quantity-input");
            startJax(addToCardBtn);
            const model = {};
            model.id = Number(product.dataset.id);
            if (quantityInput) {
                model.quantity = Number(quantityInput.value);
            } else {
                model.quantity = 1
            };
            $.ajax({
                type: "POST",
                url: '/cart/AddToCart',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    console.log(result)
                    Toast(result.toast.type, result.toast.title, result.toast.message)
                    loadCount(result.data)
                    stopJax(addToCardBtn);
                },
                error: function (e) {
                    console.log(e.responseText)
                    Toast("error", "", "Lỗi trong khi thêm vào giỏ hàng!");
                    stopJax(addToCardBtn);
                }
            });
        }
    })
    
}());