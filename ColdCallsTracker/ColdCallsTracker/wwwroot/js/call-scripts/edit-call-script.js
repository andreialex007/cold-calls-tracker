$(function () {

    window.editScript = new Vue({
        el: ".edit-script-page",
        data: function () {
            return {
                entity: $(".edit-script-page").data("json")
            }
        },
        methods: {
            savePage() {
                $("form").submit();
            }
        },
        computed: {
            isEmptyName: function () {
                return !this.entity.Name || !this.entity.Name.trim();
            }
        },
        async mounted() {

        }
    });

})