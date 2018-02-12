$(document).ready(function () {
    $('#contact_form').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {

            // Fist Name
            first_name: {
                validators: {
                    stringLength: {
                        min: 2,
                    },
                    notEmpty: {
                        message: 'Please Enter your First Name'
                    }
                }
            },
            // Last Name
            last_name: {
                validators: {
                    stringLength: {
                        min: 2,
                    },
                    notEmpty: {
                        message: 'Please Enter your Last Name'
                    }
                }
            },
            //User Name
            user_name: {
                validators: {
                    stringLength: {
                        min: 8,
                    },
                    notEmpty: {
                        message: 'Please Enter your Username'
                    }
                }
            },
            // User_Password
            user_password: {
                validators: {
                    stringLength: {
                        min: 6,
                    },
                    notEmpty: {
                        message: 'Please Enter your Password'
                    }
                }
            },

            //Confirm Password
            confirm_password: {
                validators: {
                    stringLength: {
                        min: 6,
                    },
                    notEmpty: {
                        message: 'Please Enter your Confirm Password'
                    }
                }
            },

            //E-Mail Validation
            email: {
                validators: {
                    notEmpty: {
                        message: 'Please Enter your E-Mail Address'
                    },
                    emailAddress: {
                        message: 'Please Enter a Valid Email Address'
                    }
                }
            },

        }
    }).on('success.form.bv', function (e) {
        $('#success_meesage').slideDown({ opacity: "show" }, "slow")

        $('#contact_form').data('bootstrapValidator').resetForm();

        e.preventDefault();

        var $form = $(e.target);

        var bv = $form.data('bootstrapValidator');

        //Ajax for form submition
        $.post($form.attr('action'), $form.serialize(), function (result) {
            console.log(result);
        }, 'json');
    });
});