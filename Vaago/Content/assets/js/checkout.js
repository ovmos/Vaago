$(document).ready(function () {
 
    $(".checkoutBtnfinal").click(function () {
        var checkoutName = $("#checkoutName").val();
        var checkoutEmail = $("#checkoutEmail").val();
        var checkoutPhone = $("#checkoutPhone").val();
        var checkoutCity = $("#checkoutCity").val();
        var checkoutAddress = $("#checkoutAddress").val();
        var checkoutMessage = $("#checkoutMessage").val();
        var totalBill = $("#checkoutTotalVal").html();
        var itemsCount = $("#itemsCount").html();



        var f = {}
        f.url = '/Checkout/Place_Order';
        f.type = "POST";
        f.dataType = "json";
        f.data = JSON.stringify({
            checkoutName,
            checkoutEmail,
            checkoutPhone,
            checkoutCity,
            checkoutAddress,
            checkoutMessage,
            totalBill,
            itemsCount
        });
        f.contentType = "application/json";
        f.success = () => {
            alert("success")
        }
        $.ajax(f);
        //location.href = "/Home"
    });
});