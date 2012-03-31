/*
* jQuery Html5 Form Validation v.1.1.2
* https://github.com/nunorafaelrocha/jquery-html5-form-validation
*
* Copyright 2011, Nuno Rafael Rocha
* http://nunorafaelrocha.com
*
*/


(function ($) {

    // default options
    var defaults = {
        'errorClass': 'invalid',
        onFail: function () { },
        onSuccess: function () { },
        onFailElement: function () { },
        onSuccessElement: function () { },
        extraValidation: function () { return true; },
        extraElementValidation: function () { }
    };

    var methods = {
        // initialization method
        init: function (options) {
            var options = $.extend({}, defaults, options);
            // form element
            var form = $(this);
            // save options
            form.data('options', options)
            // bind form onsubmit action
            form.bind('submit', function () {
                return form.html5formvalidation('validation');
            });
            // form elements
            form.find('input, textarea, select').each(function (argument) {
                // element
                var element = $(this);
                // save options
                element.data('options', options);
                // bind
                element.bind('keydown', function () {
                    element.data('html5formvalidation_typing', true);
                });
                element.bind('change keyup', function () {
                    element.data('html5formvalidation_typing', false);
                    if (element.data().html5formvalidation_timeout) {
                        clearTimeout(element.data().html5formvalidation_timeout);
                    };
                    element.data('html5formvalidation_timeout', setTimeout(function () { element.html5formvalidation('elementValidation', options) }, 200));
                });
            });

            $.each(options.extraElementValidation, function (key, value) {
                form.find(key).data('html5formvalidation_custom_validation', value);
            });
        },

        // validatoin on all form elements
        validation: function (options) {
            var options = $.extend({}, defaults, $(this).data().options, options);
            // indicaties if form is valid
            var is_valid = true;
            // foreach form element do the validation
            this.find('input, textarea, select').each(function () {
                is_valid = ($(this).html5formvalidation('elementValidation', options) ? is_valid : false);
            });

            !options.extraValidation() ? is_valid = false : null;

            !is_valid ? options.onFail() : options.onSuccess();

            return is_valid;
        },

        // element validation
        elementValidation: function (options) {

            var options = $.extend({}, defaults, $(this).data().options, options);
            var element = $(this);
            // is typing 
            if (element.data().html5formvalidation_typing) {
                return true;
            }

            // element type
            var type = element.attr('type');
            // indicates if element is valid
            var elem_is_valid = true;
            // remove error class (if not null or empty) from this element before validate
            options.errorClass ? element.removeClass(options.errorClass) : null;

            // if has required tag
            if (element.attr('required')) {
                // for different kind of types do different validation...
                if (type === 'checkbox' || type === 'radio') {
                    // verifies if one of the elements with this name has been checked
                    if (!$('[name*="' + element.attr('name') + '"]:checked').length) {
                        elem_is_valid = false;
                        // add error class for every elements with the same name
                        options.errorClass ? $('[name*="' + element.attr('name') + '"]').addClass(options.errorClass) : null;
                    }
                    else {
                        // removes error class for every elements with the same name
                        options.errorClass ? $('[name*="' + element.attr('name') + '"]').removeClass(options.errorClass) : null;
                    }
                }
                else {
                    // for other input types verifies if value is empty
                    $.trim(element.val()) === '' ? elem_is_valid = false : null;
                }
            }

            // if has pattern tag
            if (element.attr('pattern') && $.trim(element.val()) !== '') {
                // get the pattern
                var pattern = element.attr('pattern');
                // if pattern don't match     
                !element.val().match('^' + pattern + '$') ? elem_is_valid = false : null;
            }

            // if has a special type...
            if (type === 'email') {
                // verify if has email format
                // regular expression for email
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                // if multiple attribute
                if (element.attr('multiple')) {
                    // splitting on commas (,)
                    var emails = element.val().split(',');
                    for (var i = 0; i < emails.length; i++) {
                        (emails[i] === '' || !emailReg.test(emails[i])) ? elem_is_valid = false : null;
                    }
                }
                else {
                    !emailReg.test(element.val()) ? elem_is_valid = false : null;
                }
            }

            // extra validation
            if (element.data('html5formvalidation_custom_validation')) {
                !element.data('html5formvalidation_custom_validation')(element, elem_is_valid) ? elem_is_valid = false : null;
            }

            element.html5formvalidation('setState', elem_is_valid, options);

            return elem_is_valid;
        },

        setState: function (state, options) {
            var element = $(this);
            var options = $.extend({}, defaults, $(this).data().options, options);

            !state ? options.onFailElement() : options.onSuccessElement();

            // if element is not valid, apply the error class if not null
            !state && options.errorClass ? element.addClass(options.errorClass) : null;
        }
    };

    $.fn.html5formvalidation = function (method) {

        // Method calling logic
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.html5formvalidation');
        }

    };

})(jQuery);