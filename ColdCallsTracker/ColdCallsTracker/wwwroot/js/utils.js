$(function () {
    window.utils = function () {

        var self = {};

        self.wait = function (ms) {
            return new Promise(resolve => setTimeout(resolve, ms));
        };

        return self;


    }();
})