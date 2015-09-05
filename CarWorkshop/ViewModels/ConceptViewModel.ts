/// <reference path="../scripts/typings/jquery.d.ts" />
/// <reference path="../scripts/typings/bootstrap.d.ts" />

module Concept {

    export class ConceptViewModel {

        name = "somename";
        concept: Concept;
        scope: any;
        concepts = new Array<Concept>();
        constructor(getScope) {
            this.scope = getScope;
        }

        private showModal() {
            $("#conceptModal").modal("show");
        }

        private hideModal() {
            $("#conceptModal").modal("hide");
        }

        public refresh() {
            const url = `http:////${window.location.host}/Api/ApiConcept/get`;
            $.ajax({
                url: url,
                type: "GET",
                dataType: "json",
                success: data=> {
                    this.concepts = data;
                    this.scope.$apply();
                },
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
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

            $.ajax({
                url: `http:////${window.location.host}/Api/ApiConcept/Post`,
                type: "POST",
                dataType: "json",
                data: this.concept,
                success: data => {
                    this.hideModal();
                    this.refresh();
                },
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }

        public delete(concept: Concept) {

            $.ajax({
                url: `http:////${window.location.host}/Api/ApiConcept/Delete/${concept.Id}`,
                type: "DELETE",
                dataType: "json",
                success: data => {
                    this.refresh();
                },
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }

    }

    export class Concept {
        Id: number;
        Name: string;
        Value: string;
    }
}