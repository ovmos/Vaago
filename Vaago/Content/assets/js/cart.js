var check = false;

getTotal()
// Update the product total
function changeVal(el) {
    var qt = parseInt(el.parent().children(".qt").html());
    var price = parseInt(el.parent().children(".price").children()[1].innerHTML);
    var eq = Math.round(price * qt * 100) / 100;

    el.parent().children(".full-price").children()[1].innerHTML = (eq);

    changeTotal();
}

// Update the total Price
function changeTotal() {
    var price = 0;
    $(".itemFullPrice").each(function (index) {
        price += parseFloat($(".itemFullPrice").eq(index).html());
    });

    price = Math.round(price * 100) / 100;
    var tax = Math.round(price * 0.05 * 100) / 100;
    var shipping = parseFloat($(".shipping span").html());
    var fullPrice = Math.round((price + shipping) * 100) / 100;

    if (price == 0) {
        fullPrice = 0;
    }

    $(".subtotal span").html(price);
    $(".total span").html(fullPrice);
}

function getTotal() {
    var price = 0;
    $(".itemFullPrice").each(function (index) {
        price += parseFloat($(".itemFullPrice").eq(index).html());
    });

    price = Math.round(price * 100) / 100;
    var tax = Math.round(price * 0.05 * 100) / 100;
    var shipping = parseFloat($(".shipping span").html());
    var fullPrice = Math.round((price + shipping) * 100) / 100;
    $(".subtotal span").html(price);
    $(".total span").html(fullPrice);
}

$(document).ready(function () {
    // REMOVE PRODUCT
    $(".remove").click(function () {
        var el = $(this);
        el.parent().parent().parent().addClass("removed");
        window.setTimeout(function () {
            el.parent()
                .parent()
                .parent()
                .slideUp("fast", function () {
                    el.parent().parent().parent().remove();
                    if ($(".product").length == 0) {
                        if (check) {
                            $("#cart").html(
                                "<h1>The shop does not function, yet!</h1><p>If you liked my shopping cart, please take a second and heart this Pen on <a href='https://codepen.io/ziga-miklic/pen/xhpob'>CodePen</a>. Thank you!</p>"
                            );
                        } else {
                            $("#cart").html("<h1>No products!</h1>");
                        }
                    }
                    changeTotal();
                });
        }, 200);
    });

    // Increase Quantity
    $(".qt-plus").click(function () {
        $(this)
            .parent()
            .children(".qt")
            .html(parseInt($(this).parent().children(".qt").html()) + 1);

        $(this).parent().children(".full-price").addClass("added");

        var el = $(this);
        window.setTimeout(function () {
            el.parent().children(".full-price").removeClass("added");
            changeVal(el);
        }, 150);
    });

    // Decrease Quantity
    $(".qt-minus").click(function () {
        child = $(this).parent().children(".qt");

        if (parseInt(child.html()) > 1) {
            child.html(parseInt(child.html()) - 1);
        }

        $(this).parent().children(".full-price").addClass("minused");

        var el = $(this);
        window.setTimeout(function () {
            el.parent().children(".full-price").removeClass("minused");
            changeVal(el);
        }, 150);
    });

    window.setTimeout(function () {
        $(".is-open").removeClass("is-open");
    }, 1200);

    $(".gotoCheckout").click(function () {
        check = true;

        var totalBill = $("#totalBill")[0].innerHTML;
        var f = {}
        f.url = '/Cart/UpdateCart';
        f.type = "POST";
        f.dataType = "json";
        f.data = JSON.stringify({ totalBill: totalBill });
        f.contentType = "application/json";
        f.success = () => {
            alert("success")
        }
        $.ajax(f);
        location.href ="/Checkout"
    });
});
