/// <reference path="../scripts/typings/jquery.d.ts" />
/// <reference path="../scripts/typings/bootstrap.d.ts" />

module Concept {

    export class ConceptViewModel extends Base.BaseViewModel {

        name = "somename";
        concept: Concept;
        scope: any;
        concepts = new Array<Concept>();
        constructor(getScope) {
            super();
            this.scope = getScope;
        }

        private showModal() {
            $("#conceptModal").modal("show");
        }

        private hideModal() {
            $("#conceptModal").modal("hide");
        }


        public refresh() {
            super.getData("ApiConcept/get", data => {
                this.concepts = data;
                this.scope.$apply();
            });
        }

        public new() {
            this.concept = null;
            this.showModal();
        }

        public edit(concept: Concept) {
            this.concept = concept;
            this.showModal();
        }

        public save() {
            if (!this.scope.conceptForm.$valid) {
                alert("Opps please fix form problems first");
                return;
            }

            this.postData("ApiConcept/Post", this.concept, () => {
                this.hideModal();
                this.refresh();
            });
        }

        public delete(concept: Concept) {
            this.deleteData(`ApiConcept/Delete/${concept.Id}`, () => {
                this.refresh();
            });
        }

    }

    export class Concept {
        Id: number;
        Name: string;
        Value: string;
    }
}