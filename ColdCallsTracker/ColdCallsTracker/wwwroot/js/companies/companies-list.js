$(function () {

    var columnsObj = {};
    let cols = ["Id", "Name", "ActivityType", "WebSites", "Remarks", "PhoneNumbers", "StateId", "LastCallRecordDate"];
    for (let el of cols) {
        columnsObj[el] = {
            sorting: "",
            filter: ""
        };
    }

    window.companiesList = new Vue({
        el: ".companies-list-page",
        data: function () {

            return {
                items: [],
                columns: columnsObj,
                totalRecords: 0,
                shownRecords: 0,
                filteredRecords: 0,
                isLoading: false
            }
        },
        methods: {
            applySorting(column) {
                let sorting = column.sorting;
                for (let item in this.columns) {
                    this.columns[item].sorting = "";
                }
                column.sorting = sorting === "asc" ? "desc" : "asc";
            },
            doSearch() {

            },
            clearSearch() {
                for (let item in this.columns) {
                    this.columns[item].filter = "";
                    this.columns.LastCallRecordDate.filterFrom = "";
                    this.columns.LastCallRecordDate.filterTo = "";
                }
            }
        },
        mounted() {

            this.columns.Id.sorting = "asc";
            this.columns.LastCallRecordDate.filterFrom = "";
            this.columns.LastCallRecordDate.filterTo = "";

            let vm = this;

            $('.from-date').datepicker({
                autoClose: true,
                onSelect: function (value) { vm.columns.LastCallRecordDate.filterFrom = value; }
            });

            $('.to-date').datepicker({
                autoClose: true,
                onSelect: function (value) { vm.columns.LastCallRecordDate.filterFrom = value; }
            });
        }
    });

})