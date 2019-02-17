$(function () {

    window.companiesList = new Vue({
        el: ".company-edit-page",
        data: function () {
            return {
                entity: { Id: location.pathname.split('/')[3] },
                errorsView: "",
                activeTab: "log"
            }
        },
        methods: {
            async save() {
                let result = await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(this.entity),
                    url: "/Companies/Save"
                });

                this.entity = result;
            },
            async load() {

            }
        },
        async mounted() {

            let result = await $.ajax({
                method: "GET",
                url: "/Companies/Load?id=" + this.entity.Id
            });

            if (result.errorsView) {
                this.errorsView = result.errorsView;
                return;
            }

            this.entity = result;
        }
    });


})