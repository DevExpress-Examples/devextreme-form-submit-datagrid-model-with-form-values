import { useCallback, useRef, useState } from 'react';
import './App.css';
import 'devextreme/dist/css/dx.material.blue.light.compact.css';
import Button from 'devextreme-react/button';
import type { ButtonTypes } from 'devextreme-react/button';
import Form, {
  RequiredRule,
  SimpleItem,
  StringLengthRule,
} from 'devextreme-react/form';
import DataGrid, {
  Column,
  Editing,
  FilterRow,
  Grouping,
  GroupPanel,
  HeaderFilter,
  Paging,
} from 'devextreme-react/data-grid';
import type { DataGridRef } from 'devextreme-react/data-grid';

const customer = {
  CustomerID: 1,
  FirstName: '',
  LastName: '',
  HireDate: null,
};

const customerIdEditorOptions = { readOnly: true };
const orders: Record<string, unknown>[] = [];

function createInputElement(
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

function App(): JSX.Element {
  const formElementRef = useRef<HTMLFormElement>(null);
  const gridRef = useRef<DataGridRef>(null);
  const [payload, setPayload] = useState('');

  const onSubmit = useCallback((e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formElement = formElementRef.current;
    if (!formElement) {
      return;
    }
    const lines = [...new FormData(formElement).entries()]
      .map(([name, value]) => `${name}=${value}`);
    setPayload(`Submitted payload:\n${lines.join('\n')}`);
  }, []);

  const onButtonClick = useCallback((e: ButtonTypes.ClickEvent) => {
    if (!e.validationGroup?.validate().isValid) {
      return;
    }
    const formElement = formElementRef.current;
    const grid = gridRef.current?.instance();
    if (!formElement || !grid) {
      return;
    }
    formElement.querySelectorAll('input.order-input').forEach((el) => el.remove());
    grid.getDataSource().load().then((items: Record<string, unknown>[]) => {
      items.forEach((item, itemIndex) => {
        Object.keys(item).forEach((property) => {
          createInputElement(property, item[property], itemIndex, formElement);
        });
      });
      formElement.requestSubmit();
    });
  }, []);

  return (
    <div className="main">
      <h2>Edit Form:</h2>
      <form
        ref={formElementRef}
        method="post"
        action="#"
        onSubmit={onSubmit}
      >
        <Form formData={customer} validationGroup="customer">
          <SimpleItem
            dataField="CustomerID"
            editorType="dxNumberBox"
            editorOptions={customerIdEditorOptions}
          />
          <SimpleItem dataField="FirstName">
            <RequiredRule />
            <StringLengthRule max={10} />
          </SimpleItem>
          <SimpleItem dataField="LastName">
            <RequiredRule />
            <StringLengthRule max={10} />
          </SimpleItem>
          <SimpleItem dataField="HireDate" editorType="dxDateBox" />
        </Form>
        <DataGrid
          id="grid"
          ref={gridRef}
          dataSource={orders}
          keyExpr="OrderID"
          showBorders={true}
          dateSerializationFormat="MM-dd-yyyy"
          remoteOperations={false}
        >
          <Editing allowUpdating={true} allowAdding={true} allowDeleting={true} />
          <Paging pageSize={10} />
          <FilterRow visible={true} />
          <HeaderFilter visible={true} />
          <GroupPanel visible={true} />
          <Grouping autoExpandAll={false} />
          <Column dataField="OrderID" />
          <Column dataField="OrderDate" dataType="date" />
          <Column dataField="CustomerName" />
          <Column dataField="ShipCountry" />
          <Column dataField="ShipCity" />
        </DataGrid>
        <Button
          text="Validate and Submit"
          validationGroup="customer"
          useSubmitBehavior={false}
          onClick={onButtonClick}
        />
      </form>
      <pre id="payload">{payload}</pre>
    </div>
  );
}

export default App;
