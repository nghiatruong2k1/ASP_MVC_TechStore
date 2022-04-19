(function () {

    const cartContent = document.querySelector("#cart-content");
    const emptyData = document.querySelector("#empty-data");

    const cartCounts = document.querySelectorAll(".cart-count");

    const cartPrice = document.querySelector("#cart-price");
    const cartDiscount = document.querySelector("#cart-discount");
    const cartTotal = document.querySelector("#cart-total");

    const formVoucher = document.querySelector("#form-voucher");
    const voucherInput = document.querySelector("[name='voucher']");
    let sesstionCart = [];

    formVoucher.addEventListener("submit", function (event) {
        startJax(formVoucher);
        event.preventDefault();
        const model = {};
        model.voucher = voucherInput.value;
        $.ajax({
            type: "POST",
            url: '/cart/Voucher',
            data: JSON.stringify(model),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function ({ data, toast }) {
                console.log({ data, toast })
                voucherInput.dataset.voucher = data;
                Toast(toast.type, toast.title, toast.message)
                loadView();
                stopJax(formVoucher);
            }, error: function (e) {
                console.log(e);
               stopJax(formVoucher);
            }
        });
    })


    function loadView() {
        if (sesstionCart.length == 0) {
            emptyData.classList.add("d-flex");
            emptyData.classList.remove("d-none");
        } else {
            emptyData.classList.add("d-none");
            emptyData.classList.remove("d-flex");
        }
        const { quantity, price } = sesstionCart.reduce(function (out, { quantity,price,salePrice }) {
            out.quantity += quantity;
            if (salePrice && salePrice > 0) {
                out.price +=  quantity*salePrice;
            } else if (price && price > 0) {
                out.price +=  quantity* price;
            }
            return out;
        }, { quantity: 0, price: 0 });

        Array.from(cartCounts).forEach(function (cartcount) {
            cartcount.innerText = quantity;
        });

        cartPrice.innerText = formatNumber(price,3,0) +" đ";

        const salePrice = price * Number(voucherInput.dataset.voucher) / 100;
        cartDiscount.innerText = formatNumber(salePrice, 3, 0) + " đ";
        cartTotal.innerText = formatNumber(price - salePrice, 3, 0) + " đ";
    };

    $.ajax({
        type: "GET",
        url: '/cart/GetData',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(result) {
            sesstionCart = result;
            loadView();
        }, error: function () {
            sesstionCart = []
            loadView();
        }
    });

    if (cartContent) {
        cartContent.addEventListener("click", function (event) {
            const btn = event.target.closest(".btn");
            if (btn) {
                if (btn.classList.contains("delete-btn")) {
                    addDeleteEvent(btn,event);
                } else if (btn.classList.contains("payment-btn")) {
                    addPaymentEvent(btn,event);
                }
            }

        })
    }

    function addDeleteEvent(deleteBtn) {
        const product = deleteBtn.closest(".cart-product");
        const quantityInput = product.querySelector(".quantity-input");
        startJax(deleteBtn);
        const result = confirm("Bạn có muốn xóa sản phẩm này khỏi giỏ hàng!");
        if (result) {
            const model = {};
            model.id = Number(product.dataset.id);
            $.ajax({
                type: "POST",
                url: '/cart/Remove',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function ({ data, toast }) {
                    console.log(data,toast)
                    product.parentElement.removeChild(product);
                    sesstionCart = data;
                    loadView();
                    Toast(toast.type,toast.title,toast.message)
                    stopJax(deleteBtn);
                },
                error: function (e) {
                    console.log(e.responseText)
                    loadView();
                    Toast("error", "", "Có lỗi xãy ra!");
                    stopJax(deleteBtn);
                }
            });
        }
    }
    function addPaymentEvent(btn,event) {
        if (sesstionCart.length > 0) {
            return confirm("Bạn có muốn đặt hàng!");
        } else {
            Toast("","","Giỏ hàng của bạn rổng!")
        }
        event.preventDefault();
        return false;
    }
}());