$(function () {

    window.companiesList = new Vue({
        el: ".companies-list-page",
        data: function () {
            return {
                items: [],
                columns: [
                    { id: "", name: "" }
                ]
            }
        },
        methods: {

        },
        mounted() {

        }
    });

})