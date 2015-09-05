module Base {

    export class BaseViewModel {
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
}