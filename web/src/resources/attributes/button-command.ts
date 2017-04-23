import {Command} from "../../helper/command";
import {autoinject, ObserverLocator} from "aurelia-framework";
import * as $ from "jquery";

@autoinject()
export class ButtonCommandCustomAttribute {
    private _onCommandIsExecutingChangedBind: (newValue: any, oldValue: any) => void;        
    private _onCommandCanExecuteChangedBind: (newValue: any, oldValue: any) => void;

    private _command: Command;
                
    public constructor(private element: Element, private observerLocator: ObserverLocator) {
      this._onCommandIsExecutingChangedBind = this.onCommandIsExecutingChanged.bind(this);
      this._onCommandCanExecuteChangedBind = this.onCommandCanExecuteChanged.bind(this);      
    }
    
    public attached(): void {
        this.element.addEventListener("click", this.onButtonClick.bind(this));
    }
    
    public detached(): void {
        this.element.removeEventListener("click", this.onButtonClick.bind(this));
    }
    
    public valueChanged(newValue: Command, oldValue: Command): void {
        this._command = newValue;
        
        if (oldValue) {
            this.observerLocator
                .getObserver(oldValue, "isExecuting")
                .unsubscribe(this._onCommandIsExecutingChangedBind);         
                
            this.observerLocator
                .getObserver(oldValue, "canExecute")
                .unsubscribe(this._onCommandCanExecuteChangedBind);
        }
        
        if (newValue) {
            this.observerLocator
                .getObserver(newValue, "isExecuting")
                .subscribe(this._onCommandIsExecutingChangedBind);
                
            this.observerLocator
                .getObserver(newValue, "canExecute")
                .subscribe(this._onCommandCanExecuteChangedBind);
                
            this.onCommandIsExecutingChanged(this._command.isExecuting, undefined);
            this.onCommandCanExecuteChanged(this._command.canExecute, undefined);                
        }        
    }
    
    private onCommandIsExecutingChanged(newValue: boolean, oldValue: boolean): void {
        if (newValue === true) { 
            this.addLoading();         
        }
        if (newValue === false) {
            this.removeLoading();
        }
    }
    private onCommandCanExecuteChanged(newValue: boolean, oldValue: boolean): void {
        if (newValue === true) {
            this.removeDisabled();
        }
        if (newValue === false) {
            this.addDisabled();
        }
    }
    private async onButtonClick(): Promise<void> {
        if (this._command !== null) {
            await this._command.execute();
        }
    }

    private addLoading(): void {
        var loadingMessage = this.element.getAttribute("button-command-loading-message");

        if (!loadingMessage)
            loadingMessage = "Loading";

        this.element.setAttribute("data-loading-text", "<style>.glyphicon-spin { animation: spin 700ms infinite linear; } @keyframes spin { 0% { transform: rotate(0deg); } 100% { transform: rotate(359deg); } }</style><span class='glyphicon glyphicon-refresh glyphicon-spin'></span> " + loadingMessage + "…");
        $(this.element).button("loading");
    }
    private removeLoading(): void {
        $(this.element).button("reset");
        this.element.removeAttribute("data-loading-text");

        //Make sure the button is correctly disabled when we finished executing the command
        setTimeout(() => {
            this.onCommandCanExecuteChanged(this._command.canExecute, undefined);
        }, 0);
    }

    private addDisabled(): void {
        this.element.classList.add("disabled");
    }
    private removeDisabled(): void {
        this.element.classList.remove("disabled");
    }
}