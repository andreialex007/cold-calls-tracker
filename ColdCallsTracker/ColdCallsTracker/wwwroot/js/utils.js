$(function () {
    window.utils = function () {

        var self = {};

        self.wait = function (ms, timeoutId) {
            if (!!timeoutId) {
                window.timeouts = window.timeouts || {};

                return new Promise(resolve => {
                    clearTimeout(window.timeouts[timeoutId]);
                    window.timeouts[timeoutId] = setTimeout(resolve, ms);
                });
            }
            return new Promise(resolve => setTimeout(resolve, ms));
        };

        return self;


    }();
})