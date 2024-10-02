export abstract class Urls {
    private static readonly IsInProd: boolean = false;
    static readonly ApiUrl: string = this.IsInProd ? "http://motodev-api.space" : "https://localhost:5078";
}