import {Command} from "../../helper/command";
import {autoinject, ObserverLocator} from "aurelia-framework";

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
            this.element.classList.add("disabled");
        }
        if (newValue === false) {
            this.element.classList.remove("disabled");
        }
    }
    private onCommandCanExecuteChanged(newValue: boolean, oldValue: boolean): void {
        if (newValue === true) {
            this.element.classList.remove("disabled");
        }
        if (newValue === false) {
            this.element.classList.add("disabled");
        }
    }
    private async onButtonClick(): Promise<void> {
        if (this._command !== null) {
            await this._command.execute();
        }
    }
}