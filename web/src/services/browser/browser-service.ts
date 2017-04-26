export class BrowserService {
    
    public currentUrl(): string {
        return window.location.href;
    }

    public goToUrl(url: string): string {
        return window.location.href = url;
    }
}