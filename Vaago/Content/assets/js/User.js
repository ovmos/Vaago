function editProfile() {
    $(".value-class").removeAttr("disabled");
}

function updateProfile() {
    $(".value-class").attr("disabled");
    window.location.reload();
}