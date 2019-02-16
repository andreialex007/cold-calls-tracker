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
                filteredRecords: 0,
                isLoading: false,
                skip: 0,
                take: 50,
                isEndofList: false
            }
        },
        methods: {
            applySorting(column) {
                let sorting = column.sorting;
                for (let item in this.columns) {
                    this.columns[item].sorting = "";
                }
                column.sorting = sorting === "asc" ? "desc" : "asc";

                this.doSearch(true);
            },
            async doSearch(reset) {

                if (reset) {
                    this.skip = 0;
                }

                var params = {};
                for (let item in this.columns) {
                    let column = this.columns[item];
                    params[item] = column.filter;
                    if (!!column.sorting) {
                        params.OrderBy = item;
                        params.IsAsc = column.sorting == "asc";
                    }
                }
                params.LastCallRecordDateFrom = this.columns.LastCallRecordDate.filterFrom;
                params.LastCallRecordDateTo = this.columns.LastCallRecordDate.filterTo;
                params.Take = this.take;
                params.Skip = this.skip;

                this.isLoading = true;

                let result = await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(params),
                    url: "/Companies/Search"
                });

                this.isLoading = false;

                this.items = reset ? result.items : this.items.concat(result.items);
                this.totalRecords = result.total;
                this.filteredRecords = result.filtered;
                this.isEndofList = this.items.length >= this.totalRecords || result.items.length < this.take;

            },
            loadMore() {
                this.skip += this.take;
                this.doSearch();
            },
            clearSearch() {
                for (let item in this.columns) {
                    this.columns[item].filter = "";
                    this.columns.LastCallRecordDate.filterFrom = "";
                    this.columns.LastCallRecordDate.filterTo = "";
                }
                this.doSearch(true);
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

            this.doSearch();
        }
    });

})