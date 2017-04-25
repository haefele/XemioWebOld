export class OAuthHelper {
        
    public showOAuthPopup(name: string, uri: string, redirectUri: string) : Promise<string> {
        let popupWindow : Window = window.open(uri, name);
        
        return new Promise((resolve, reject) => {
            let polling = setInterval(() => {
                try {                   
                    if (popupWindow.location.href.indexOf(redirectUri) === 0) {                       
                        clearInterval(polling);
                        popupWindow.close();
                        
                        resolve(popupWindow.location.href);
                    }
                } 
                catch (e) { 
                    reject(e);
                }
            }, 35);
        });
    }    
}