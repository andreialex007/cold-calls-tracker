$(function () {

    window.editQuoteTemplate = new Vue({
        el: ".edit-quote-template",
        data: function () {
            return {
                ...$(".edit-quote-template").data("json"),
                showOnlyChecked: true
            };
        },
        mounted() {

        },
        computed: {
            isEmptyName: function () {
                return !this.Name || !this.Name.trim();
            }
        },
        methods: {
            getRelation(costing) {
                return this.QuoteCostingRelations.filter(x => x.CostingTemplateId === costing.Id)[0];
            },
            checkCosting(costing) {
                let relation = this.getRelation(costing);
                if (!!relation) {
                    this.deleteRelation(costing);
                } else {
                    this.addRelation(costing);
                }
            },
            isCheckedCosting(costing) {
                return !!this.getRelation(costing);
            },
            async addRelation(costing) {
                var newRelation = {
                    QuoteTemplateId: this.Id,
                    QuoteTemplate: null,
                    CostingTemplateId: costing.Id,
                    CostingTemplate: null,
                    Multiplier: 1
                }

                await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(newRelation),
                    url: "/QuoteTemplates/AddRelation",
                    dataType: "JSON"
                });

                this.QuoteCostingRelations.push(newRelation);
            },
            async deleteRelation(costing) {
                let relation = this.getRelation(costing);

                await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({
                        QuoteTemplateId: relation.QuoteTemplateId,
                        CostingTemplateId: relation.CostingTemplateId
                    }),
                    url: "/QuoteTemplates/DeleteRelation",
                    dataType: "JSON"
                });
                this.QuoteCostingRelations = this.QuoteCostingRelations.filter(x => x.CostingTemplateId !== costing.Id);
            },
            saveTemplate() {
                $("form").submit();
            }
        }
    });


});