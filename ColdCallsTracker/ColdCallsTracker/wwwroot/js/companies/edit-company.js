﻿$(function () {

    Vue.directive('mask', {
        bind: function (el, binding) {
            window.Inputmask(binding.value).mask(el);
        }
    });


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
                window.editPhoneModal.entity.CompanyId = this.entity.Id;
                window.editPhoneModal.open();
            },
            editPhone(item) {
                window.editPhoneModal.entity = JSON.parse(JSON.stringify(editCompany.entity.Phones.filter(x => x.Id == editCompany.selectedPhoneId)[0]));
                window.editPhoneModal.open();
                window.editPhoneModal.isComplete = true;
            },
            removePhone(item) {
                
            }
        },
        async mounted() {

            let vm = this;

            window.onPhoneChange = function () {
                let entity = window.editPhoneModal.entity;
                vm.entity.Phones = vm.entity.Phones.filter(x => x.Id !== entity.Id);
                vm.entity.Phones.push(entity);
                vm.entity.Phones = _.sortBy(vm.entity.Phones, function (x) { return x.Number; });
            }

            let result = await $.ajax({
                method: "GET",
                url: "/Companies/Load?id=" + this.entity.Id
            });

            if (result.errorsView) {
                this.errorsView = result.errorsView;
                return;
            }

            this.entity = result;

            if (this.entity.Phones.length !== 0) {
                this.selectedPhoneId = this.entity.Phones[0].Id + "";
            }
        }
    });

    window.editPhoneModal = new Vue({
        el: ".edit-phone-modal",
        data: function () {
            return {
                isComplete: false,
                visible: false,
                hasDuplicates: false,
                duplicate: { number: "", company: "" },
                companyEditLink: "",
                entity: {
                    Number: "8 (___) ___-__-__",
                    Remarks: "Общий",
                    CompanyId: null,
                    Id: 0
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
            async save() {
                if (!this.isReadyForSave)
                    return;

                let result = await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(this.entity),
                    url: "/Companies/EditPhone"
                });

                this.entity = result;
                window.onPhoneChange();
                this.close();
            },
            maskCheck: function (field) {
                this.isComplete = field.target.inputmask.isComplete();
                this.entity.Number = $(field.target).val();
                if (!this.isComplete) {
                    this.hasDuplicates = false;
                }
            }
        },
        computed: {
            isReadyForSave: function () {
                return !this.hasDuplicates && this.isValid;
            },
            isValid: function () {
                return !!this.entity.Remarks && this.isComplete;
            }
        },
        watch: {
            'entity.Number': async function () {

                if (!this.isComplete)
                    return;

                let result = await $.ajax({
                    method: "GET",
                    url: "/Companies/FindPhoneDuplicate?id=" + this.entity.Id + "&phone=" + this.entity.Number
                });

                this.hasDuplicates = result.hasDuplicate;
                if (this.hasDuplicates) {
                    this.duplicate.number = result.number;
                    this.duplicate.company = result.company;
                    this.companyEditLink = "/Companies/Edit/" + result.CompanyId;
                }
            }
        }
    });


})