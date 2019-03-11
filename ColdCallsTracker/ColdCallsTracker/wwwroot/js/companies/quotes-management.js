$(function () {

    window.quotesManagement = {
        data: function () {
            return {
                managementProperty: ""
            }
        },
        mounted: function () {



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
                    url: "/Companies/Rename?quoteId=" + quote.Id + "&name=" + encodeURIComponent(quote.Name)
                });
            }
        }
    };


})