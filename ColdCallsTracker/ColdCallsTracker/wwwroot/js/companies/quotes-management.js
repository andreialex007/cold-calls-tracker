$(function () {


    let defaultCosting = {
        Id: 0,
        Name: "",
        Unit: 1,
        Qty: 1,
        Cost: "",
        Total: "",
        Multiplier: 1,
        CategoryId: 1
    };

    window.quotesManagement = {
        data: function () {
            return {
                managementProperty: "",
                newCosting: {
                    ...defaultCosting
                }
            }
        },
        mounted: function () {


        },
        computed: {
            addCostingBtnActive() {
                return this.costingValid(this.newCosting);
            }
        },
        methods: {
            async newQuote() {
                let newQuote = await $.ajax({
                    method: "GET",
                    contentType: "application/json",
                    url: "/Companies/NewEmptyQuote?companyId=" + this.entity.Id
                });

                this.entity.Quotes.push(newQuote);
            },
            renameQuote(quote) {
                $.ajax({
                    method: "GET",
                    contentType: "application/json",
                    url: "/Companies/RenameQuote?quoteId=" + quote.Id + "&name=" + encodeURIComponent(quote.Name)
                });
            },
            async addCosting(quote) {
                let costing = this.newCosting;
                costing.QuoteId = quote.Id;

                let result = await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(costing),
                    url: "/Companies/SaveCosting"
                });

                quote.Costings.push(result);
                this.newCosting = {
                    ...defaultCosting
                };
            },
            async costingChanged(costing) {
                if (!this.costingValid(costing))
                    return;

                await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(costing),
                    url: "/Companies/SaveCosting"
                });
            },
            async deleteCosting(costing, quote) {

                let confirmed = confirm("Хотите удалить затрату?");
                if (!confirmed)
                    return;

                await $.ajax({
                    method: "GET",
                    contentType: "application/json",
                    url: "/Companies/DeleteCosting?id=" + costing.Id
                });
                quote.Costings = quote.Costings.filter(x => x.Id !== costing.Id);

            },
            costingValid(costing) {
                if (!costing.Name) return false;
                if (!costing.Qty) return false;
                if (!costing.Multiplier) return false;
                return true;
            },
            async  addQuoteFromTemplate(templateId) {
                let newQuote = await $.ajax({
                    method: "GET",
                    contentType: "application/json",
                    url: "/Companies/AddQuoteFromTemplate?templateId=" + templateId + "&companyId=" + this.entity.Id
                });

                this.entity.Quotes.push(newQuote);
            }
        }
    };


})