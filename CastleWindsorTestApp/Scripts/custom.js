jQuery(document).on("click", '#SendForm', function (jsonData) {

    jQuery("form").removeData("validator");
    jQuery("form").removeData("unobtrusiveValidation");
    jQuery.validator.unobtrusive.parse("form");

    if (jQuery("form").valid()) {
        var data = jQuery("form").serialize();
        jQuery.ajax({
            url: 'Employee/' + jsonData.currentTarget.name,
            type: 'POST',
            data: data,
            success: function (response) {
                alertify.set('notifier', 'position', 'top-right');
                if (response.success) {
                    alertify.success("The employee has been successfully " + response.success);
                    $('#myModal').modal('toggle');
                    setTimeout(function () {
                        location.reload();
                    }, 1500);
                }
                else
                    alertify.error(response.error);


            },
            error: function (response) {
                alertify.error(response);
            }
        });
    }
});
