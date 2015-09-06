/// <reference path="../scripts/typings/jquery.d.ts" />
/// <reference path="../scripts/typings/bootstrap.d.ts" />

module SparePartJob {

    export class SparePartJobViewModel extends Base.BaseViewModel {

        entity: SparePartJob;
        scope: any;
        entities = new Array<SparePartJob>();

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
            super.getData("ApiSparePartJob/get", data => {
                this.entities = data;
                this.scope.$apply();
            });
        }

        public new() {
            this.entity = null;
            this.showModal();
        }

        public edit(toEdit: SparePartJob) {
            this.entity = toEdit;
            this.showModal();
        }

        public save() {
            if (!this.scope.conceptForm.$valid) {
                alert("Opps please fix form problems first");
                return;
            }

            this.postData("ApiConcept/Post", this.entities, () => {
                this.hideModal();
                this.refresh();
            });
        }

        public delete(concept: SparePartJob) {
            this.deleteData(`ApiConcept/Delete/${concept.Id}`, () => {
                this.refresh();
            });
        }

    }

    export class Lookup {
        Id: number;
        ArabicName: string;
        EnglishName: string;
        Notes: string;
    }

    export class SparePartJob {
        Id: number;
        Car: Car;
        SpareParts: Array<SparePart>;
        Price: number;
    }

    export class Car {
        Id: number;
        CarNumber: string;
        CarType: CarType;
        CarModel: CarModel;
    }

    export class CarType extends Lookup {}
    export class CarModel extends Lookup {}
    export class SparePart extends Lookup {}

}