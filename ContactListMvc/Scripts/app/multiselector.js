﻿var multiselector = (function () {
    function multiselector(element, nonSelectedText, enableFiltering, url) {
        this.nonSelectedText = nonSelectedText;
        this.element = element;
        this.enableFiltering = enableFiltering;
        this.url = url;

        this.init();
    }

    multiselector.prototype.init = function () {
        var self = this;

        if (typeof (this.url) !== 'undefined') {
            $.get(this.url, function (data) {
                self.done(data);
            });
        }  
    }

    multiselector.prototype.done = function (data) {
        var self = this;

        if (typeof (data) !== 'undefined') {
            $.each(data, function (key, value) {
                self.element.append($('<option />').val(value.name).text(value.name));
            });

            this.start();
        }        
    }

    multiselector.prototype.start = function () {
        this.element.multiselect({
            enableFiltering: this.enableFiltering,
            nonSelectedText: this.nonSelectedText,
            nSelectedText: "выбрано",
            enableCaseInsensitiveFiltering: true
        });
    }


    return multiselector;
})();