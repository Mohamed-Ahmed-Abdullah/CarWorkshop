/// <reference path="../scripts/typings/jquery.d.ts" />
/// <reference path="../scripts/typings/bootstrap.d.ts" />

module SparePartJob {
    import KeyValue = Base.KeyValue;

    export class SparePartJobViewModel extends Base.BaseViewModel {

        entity: SparePartJob;
        scope: any;
        entities = new Array<SparePartJob>();

        carTypes = new Array<CarType>();
        carModels = new Array<CarModel>();

        pages = new Array<KeyValue>();
        currentPage: KeyValue;
        itemsPerPage = 10;

        newCarType: string;
        newCarModel: string;

        constructor(getScope) {
            super();
            this.scope = getScope;            

            //Get Lookups
            super.getData("ApiSparePartJob/GetCarTypes", data => {
                this.carTypes = data;
                this.scope.$apply();
            });

            super.getData("ApiSparePartJob/GetCarModels", data => {
                this.carModels = data;
                this.scope.$apply();
            });

            this.getPagesCount();

            super.getData("ApiSparePartJob/GetSpareParts", data => {
                (<any>$("#mySingleFieldTags")).tagit({
                    availableTags: data,
                    allowSpaces: true
                });
            });

        }

        private showModal() {
            $("#conceptModal").modal("show");
        }

        private hideModal() {
            $("#conceptModal").modal("hide");
        }

        private getPagesCount() {
            super.getPages("ApiSparePartJob/GetPagesCount", this.itemsPerPage, (pages) => {
                this.pages = pages;

                this.refresh(this.currentPage == null ? pages[0] : this.currentPage);
            });
        }

        public addCarType() {
            this.postData(`ApiSparePartJob/AddCarType?carType=${this.newCarType}`, this.entity, () => {
                //update lookups
                super.getData("ApiSparePartJob/GetCarTypes", data => {
                    this.carTypes = data;

                    //select it
                    $.each(this.carTypes, (index, value) => {
                        if (value.ArabicName === this.newCarType)
                            this.entity.Car.CarType = value;
                    });

                    this.scope.$apply();

                });
            });
        }

        public addCarModel() {
            this.postData(`ApiSparePartJob/AddCarModel?carModel=${this.newCarModel}`, this.entity, () => {
                //update lookups
                super.getData("ApiSparePartJob/GetCarModels", data => {
                    this.carModels = data;

                    //select it
                    $.each(this.carModels, (index, value) => {
                        if (value.ArabicName === this.newCarModel)
                            this.entity.Car.CarModel = value;
                    });

                    this.scope.$apply();

                });
            });
        }

        public changeCurrentPage(page: KeyValue) {
            this.currentPage = page;

            $.each(this.pages, (index, value) => {
                value.isSelected = false;
                if (value.value === page.value)
                    value.isSelected = true;
            });
            this.scope.$apply();
        }

        public refresh(page: KeyValue) {

            super.getList("ApiSparePartJob/get", this.itemsPerPage, page.key, data => {
                this.entities = data;
                this.scope.$apply();

                this.changeCurrentPage(page);
            });
        }

        public new() {
            this.entity = new SparePartJob();
            this.entity.Car = new Car();
            this.entity.Car.CarType = new CarType();
            (<any>$("#mySingleFieldTags")).tagit("removeAll");

            this.showModal();
        }

        public edit(toEdit: SparePartJob) {
            this.entity = $.extend(true, {}, toEdit);

            (<any>$("#mySingleFieldTags")).tagit("removeAll");
            $.each(this.entity.SpareParts, (index, value) => {
                (<any>$("#mySingleFieldTags")).tagit("createTag", value.ArabicName);
            });
            this.showModal();
        }

        public save() {
            if (!this.scope.conceptForm.$valid) {
                alert("هنالك بعض الاخطاء في الادخال الرجاء المراجعة أولاً");
                return;
            }

            this.postData("ApiSparePartJob/Post", this.entity, () => {
                this.hideModal();

                this.refresh(this.currentPage);

                this.getPagesCount();
            });
        }

        public delete(concept: SparePartJob) {
            this.deleteData(`ApiSparePartJob/Delete/${concept.Id}`, () => {
                this.refresh(this.currentPage);
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
        SparePartsTags: string;
        Price: number;
    }

    export class Car {
        Id: number;
        CarNumber: string;
        CarType: CarType;
        CarModel: CarModel;
    }

    export class CarType extends Lookup { }
    export class CarModel extends Lookup { }
    export class SparePart extends Lookup { }
}