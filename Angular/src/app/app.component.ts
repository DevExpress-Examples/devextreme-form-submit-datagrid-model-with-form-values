import { Component, ElementRef, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { DxButtonModule, DxButtonTypes } from 'devextreme-angular/ui/button';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxDataGridComponent, DxDataGridModule } from 'devextreme-angular/ui/data-grid';

@Component({
    selector: 'app-root',
    imports: [DxButtonModule, DxFormModule, DxDataGridModule],
    templateUrl: './app.component.html',
    changeDetection: ChangeDetectionStrategy.Eager,
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  @ViewChild('editEmployee', { static: false })
  formElement!: ElementRef<HTMLFormElement>;

  @ViewChild(DxDataGridComponent, { static: false })
  grid!: DxDataGridComponent;

  customer = {
    CustomerID: 1,
    FirstName: '',
    LastName: '',
    HireDate: null,
  };

  customerIdEditorOptions = { readOnly: true };

  orders: Record<string, unknown>[] = [];

  payload = '';

  onSubmit(e: Event): void {
    e.preventDefault();
    const formElement = this.formElement.nativeElement;
    const lines = [...new FormData(formElement).entries()]
      .map(([name, value]) => `${name}=${value}`);
    this.payload = `Submitted payload:\n${lines.join('\n')}`;
  }

  onButtonClick(e: DxButtonTypes.ClickEvent): void {
    if (!e.validationGroup?.validate().isValid) {
      return;
    }
    const formElement = this.formElement.nativeElement;
    const grid = this.grid.instance;
    formElement.querySelectorAll('input.order-input').forEach((el) => el.remove());
    grid.getDataSource().load().then((items: Record<string, unknown>[]) => {
      items.forEach((item, itemIndex) => {
        Object.keys(item).forEach((property) => {
          this.createInputElement(property, item[property], itemIndex, formElement);
        });
      });
      formElement.requestSubmit();
    });
  }

  private createInputElement(
    itemName: string,
    itemValue: unknown,
    itemIndex: number,
    container: HTMLFormElement,
  ): void {
    const input = document.createElement('input');
    input.type = 'hidden';
    input.className = 'order-input';
    input.name = `Orders[${itemIndex}].${itemName}`;
    input.value = String(itemValue ?? '');
    container.appendChild(input);
  }
}
