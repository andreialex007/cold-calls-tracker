$(function () {

    window.editQuoteTemplate = new Vue({
        el: ".edit-quote-template",
        data: function () {
            return {
                ...$(".edit-quote-template").data("json")
            };
        },
        mounted() {

        },
        methods: {
            getRelation(costing) {
                return this.QuoteCostingRelations.filter(x => x.CostingTemplateId === costing.Id)[0];
            },
            checkCosting(costing) {
                let relation = this.getRelation(costing);
                if (!!relation) {
                    this.QuoteCostingRelations =
                        this.QuoteCostingRelations.filter(x => x.CostingTemplateId !== costing.Id);
                } else {
                    var newRelation = {
                        QuoteTemplateId: this.Id,
                        QuoteTemplate: null,
                        CostingTemplateId: costing.Id,
                        CostingTemplate: null,
                        Multiplier: 1
                    }
                    this.QuoteCostingRelations.push(newRelation);
                }
            },
            isCheckedCosting(costing) {
                return !!this.getRelation(costing);
            }
        }
    });


});