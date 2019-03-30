$(function () {

    window.editScript = new Vue({
        el: ".edit-script-page",
        data: function () {
            return {
                entity: $(".edit-script-page").data("json"),
                selectedQuestionId: null
            }
        },
        methods: {
            savePage() {
                $("form").submit();
            },
            addQuestion() {
                window.editQuestionModal.$data.Text = "Новый вопрос";
                window.editQuestionModal.$data.Id = 0;
                window.editQuestionModal.$data.CallScriptId = this.entity.Id;
                window.editQuestionModal.show();
            },
            editQuestion(item) {
                Object.assign(window.editQuestionModal.$data, item);
                window.editQuestionModal.show();
            }
        },
        computed: {
            isEmptyName: function () {
                return !this.entity.Name || !this.entity.Name.trim();
            },
            selectedQuestion() {
                if (!this.selectedQuestionId)
                    return null;
                let entity = this.entity.CallQuestions.filter(x => x.Id == this.selectedQuestionId)[0];
                return entity;
            }
        },
        async mounted() {

            await utils.wait(300);
            window.editQuestionModal.saveCompleted = async function (item) {
                let result = await $.ajax({
                    method: "GET",
                    url: "/CallScripts/Load?id=" + item.Id,
                    dataType: "JSON"
                });
                window.editScript.$data.entity = result;
            }
        }
    });

    window.editQuestionModal = new Vue({
        el: ".edit-question-modal",
        data: function () {
            return {
                Id: 0,
                Text: "Новый вопрос",
                visible: false,
                saveCompleted: function () { }
            }
        },
        methods: {
            async  save() {
                let result = await $.ajax({
                    method: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(this.$data),
                    url: "/CallScripts/EditQuestion",
                    dataType: "JSON"
                });
                this.$data.Id = result.Id;
                this.saveCompleted(result);
                this.close();
            },
            close() {
                this.visible = false;
            },
            show() {
                setTimeout(function () {
                    $(".question-text").focus();
                }, 100);
                this.visible = true;
            }
        },
        computed: {
            isEmptyName: function () {
                return !this.Text || !this.Text.trim();
            },
        },
        async mounted() {

        }
    });

})