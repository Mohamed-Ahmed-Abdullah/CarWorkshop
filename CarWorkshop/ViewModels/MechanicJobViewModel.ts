/// <reference path="../scripts/typings/jquery.d.ts" />
/// <reference path="../scripts/typings/bootstrap.d.ts" />

module MechanicJob {
    import KeyValue = Base.KeyValue;
    import CarType = Base.CarType;
    import CarModel = Base.CarModel;
    import Car = Base.Car;
    import Lookup = Base.Lookup;

    export class MechanicJobViewModel extends Base.BaseViewModel {

        entity: SparePartJob;
        scope: any;
        entities = new Array<SparePartJob>();

        carTypes = new Array<CarType>();
        carModels = new Array<CarModel>();
        mechanicJobTypes = new Array<MechanicJobType>();

        pages = new Array<KeyValue>();
        currentPage: KeyValue;
        itemsPerPage = 10;

        newCarType: string;
        newCarModel: string;
        newMechanicJobType: string;

        constructor(getScope) {
            super();
            this.scope = getScope;            

            //Get Lookups
            super.getData("ApiCar/GetCarTypes", data => {
                this.carTypes = data;
                this.scope.$apply();
            });

            super.getData("ApiCar/GetCarModels", data => {
                this.carModels = data;
                this.scope.$apply();
            });

            super.getData("ApiMechanicJob/GetMechanicJobTypes", data => {
                this.mechanicJobTypes = data;
                this.scope.$apply();
            });

            this.getPagesCount();
        }

        private showModal() {
            $("#conceptModal").modal("show");
        }

        private hideModal() {
            $("#conceptModal").modal("hide");
        }

        private getPagesCount() {
            super.getPages("ApiMechanicJob/GetPagesCount", this.itemsPerPage,"", (pages) => {
                this.pages = pages;

                this.refresh(this.currentPage == null ? pages[0] : this.currentPage);
            });
        }

        public addCarType() {
            this.postData(`ApiCar/AddCarType?carType=${this.newCarType}`, this.entity, () => {
                //update lookups
                super.getData("ApiCar/GetCarTypes", data => {
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
            this.postData(`ApiCar/AddCarModel?carModel=${this.newCarModel}`, this.entity, () => {
                //update lookups
                super.getData("ApiCar/GetCarModels", data => {
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

        public addMechanicJobType() {
            this.postData(`ApiMechanicJob/AddMechanicJobType?mechanicJobType=${this.newMechanicJobType}`, this.entity, () => {
                //update lookups
                super.getData("ApiMechanicJob/GetMechanicJobTypes", data => {
                    this.mechanicJobTypes = data;

                    //select it
                    $.each(this.mechanicJobTypes, (index, value) => {
                        if (value.ArabicName === this.newMechanicJobType)
                            this.entity.MechanicType = value;
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

            super.getList("ApiMechanicJob/get", this.itemsPerPage,"", page.key, data => {
                this.entities = data;
                this.scope.$apply();

                this.changeCurrentPage(page);
            });
        }

        public new() {
            this.entity = new SparePartJob();
            this.entity.Car = new Car();
            this.entity.Car.CarType = new CarType();            

            this.showModal();
        }

        public edit(toEdit: SparePartJob) {
            this.entity = $.extend(true, {}, toEdit);
            this.showModal();
        }

        public save() {
            if (!this.scope.conceptForm.$valid) {
                alert("هنالك بعض الاخطاء في الادخال الرجاء المراجعة أولاً");
                return;
            }

            this.postData("ApiMechanicJob/Post", this.entity, () => {
                this.hideModal();

                this.refresh(this.currentPage);

                this.getPagesCount();
            });
        }

        public delete(concept: SparePartJob) {
            this.deleteData(`ApiMechanicJob/Delete/${concept.Id}`, () => {
                this.refresh(this.currentPage);
            });
        }

    }

    export class SparePartJob {
        Id: number;
        Car: Car;
        MechanicType: MechanicJobType;
        SparePartsTags: string;
        Price: number;
    }
    
    export class MechanicJobType extends Lookup { }
}