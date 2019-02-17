$(function () {

    window.editCompany = new Vue({
        el: ".company-edit-page",
        data: function () {
            return {
                entity: {
                    Id: location.pathname.split('/')[3],
                    Phones: []
                },
                errorsView: "",
                activeTab: "log",


                callDescription: "",
                selectedPhoneId: null
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

            },
            addPhone() {
                window.editPhoneModal.open();
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


    window.editPhoneModal = new Vue({
        el: ".edit-phone-modal",
        data: function () {
            return {
                visible: false,
                entity: {
                    Number: "",
                    Remarks: "Общий"
                }
            }
        },
        methods: {
            open() {
                this.visible = true;

                setTimeout(function () {
                    $(".phone-input").focus();
                }, 100);
            },
            close() {
                this.visible = false;
            },
            save() {
                this.close();
            }
        }
    });


})