import { Command } from "helper/command";
import { autoinject, BindingEngine, Disposable } from "aurelia-framework";
import * as $ from "jquery";

@autoinject()
export class ButtonCommandCustomAttribute {
  private canExecuteSubscription: Disposable;
  private isExecutingSubscription: Disposable;
  private onButtonClickBind: () => Promise<void>;

  private command: Command;

  public constructor(private element: Element, private bindingEngine: BindingEngine) {
    this.onButtonClickBind = this.onButtonClick.bind(this);
  }

  public attached(): void {
    this.element.addEventListener("click", this.onButtonClickBind);
  }

  public detached(): void {
    this.element.removeEventListener("click", this.onButtonClickBind);
  }

  public valueChanged(newValue: Command, oldValue: Command): void {
    this.command = newValue;

    if (oldValue) {
      this.isExecutingSubscription.dispose();
      this.canExecuteSubscription.dispose();
    }

    if (newValue) {
      this.isExecutingSubscription = this.bindingEngine
        .propertyObserver(newValue, "isExecuting")
        .subscribe((newValue, oldValue) => this.onCommandIsExecutingChanged(newValue, oldValue));

      this.canExecuteSubscription = this.bindingEngine
        .propertyObserver(newValue, "canExecute")
        .subscribe((newValue, oldValue) => this.onCommandCanExecuteChanged(newValue, oldValue));

      this.onCommandIsExecutingChanged(this.command.isExecuting, undefined);
      this.onCommandCanExecuteChanged(this.command.canExecute, undefined);
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
    if (this.command !== null) {
      await this.command.execute();
    }
  }

  private addLoading(): void {
    let loadingMessage = this.element.getAttribute("button-command-loading-message");

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
      this.onCommandCanExecuteChanged(this.command.canExecute, undefined);
    }, 0);
  }

  private addDisabled(): void {
    this.element.classList.add("disabled");
  }
  private removeDisabled(): void {
    this.element.classList.remove("disabled");
  }
}
