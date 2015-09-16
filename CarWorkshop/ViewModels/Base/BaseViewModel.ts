module Base {

    export class BaseViewModel {

        protected getPages(url: string, itemsPerPage: number, done: (pages: Array<KeyValue>) => void) {

            $.ajax({
                url: `http:////${window.location.host}/Api/` + url + "?itemsPerPage=" + itemsPerPage,
                type: "GET",
                dataType: "json",
                success: (data) =>
                {
                    var pages = new Array<KeyValue>();
                    for (let i = 1; i < data+1; i++) {
                        var keyvalue = new KeyValue();
                        keyvalue.key = i;
                        keyvalue.value = i + "";
                        pages.push(keyvalue);
                    }
                    done(pages);
                },
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }

        protected getData(url: string, done: (data: any) => void) {
            $.ajax({
                url: `http:////${window.location.host}/Api/` + url,
                type: "GET",
                dataType: "json",
                success: done,
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }

        protected getList(url: string, itemsPerPage: number, currentPage:number, done: (data: any) => void) {
            $.ajax({
                url: `http:////${window.location.host}/Api/` + url + "?itemsPerPage=" + itemsPerPage + "&currentPage=" + currentPage,
                type: "GET",
                dataType: "json",
                success: done,
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }


        protected  postData(url: string, data: any, done: () => void) {
            $.ajax({
                url: `http:////${window.location.host}/Api/` + url,
                type: "POST",
                dataType: "json",
                data: data,
                success: done,
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }

        protected  deleteData(url: string, done: () => void) {
            $.ajax({
                url: `http:////${window.location.host}/Api/` + url,
                type: "DELETE",
                dataType: "json",
                success: done,
                error: (x, y, z) => {
                    alert(`ERROR:${x}${y}${z}`);
                }
            });
        }
    }

    export class KeyValue {
        key: number;
        value: string;
        isSelected = false;
    }

    export class Lookup {
        Id: number;
        ArabicName: string;
        EnglishName: string;
        Notes: string;
    }

    export class Car {
        Id: number;
        CarNumber: string;
        CarType: CarType;
        CarModel: CarModel;
    }

    export class CarType extends Lookup { }
    export class CarModel extends Lookup { }
}