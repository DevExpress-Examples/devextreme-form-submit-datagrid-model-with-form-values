<script setup lang="ts">
import { ref } from 'vue';

import 'devextreme/dist/css/dx.material.blue.light.compact.css';
import DxButton from 'devextreme-vue/button';
import type { ClickEvent } from 'devextreme/ui/button';
import {
  DxForm,
  DxRequiredRule,
  DxSimpleItem,
  DxStringLengthRule,
} from 'devextreme-vue/form';
import {
  DxColumn,
  DxDataGrid,
  DxEditing,
  DxFilterRow,
  DxGrouping,
  DxGroupPanel,
  DxHeaderFilter,
  DxPaging,
} from 'devextreme-vue/data-grid';

const customer = {
  CustomerID: 1,
  FirstName: '',
  LastName: '',
  HireDate: null,
};

const customerIdEditorOptions = { readOnly: true };
const orders: Record<string, unknown>[] = [];

const formElementRef = ref<HTMLFormElement>();
const gridRef = ref<InstanceType<typeof DxDataGrid>>();
const payload = ref('');

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

function onSubmit(e: Event): void {
  e.preventDefault();
  const formElement = formElementRef.value;
  if (!formElement) {
    return;
  }
  const lines = [...new FormData(formElement).entries()]
    .map(([name, value]) => `${name}=${value}`);
  payload.value = `Submitted payload:\n${lines.join('\n')}`;
}

function onButtonClick(e: ClickEvent): void {
  if (!e.validationGroup?.validate().isValid) {
    return;
  }
  const formElement = formElementRef.value;
  const grid = gridRef.value?.instance;
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
}
</script>
<template>
  <div>
    <h2>Edit Form:</h2>
    <form
      ref="formElementRef"
      method="post"
      action="#"
      @submit="onSubmit"
    >
      <DxForm
        :form-data="customer"
        validation-group="customer"
      >
        <DxSimpleItem
          data-field="CustomerID"
          editor-type="dxNumberBox"
          :editor-options="customerIdEditorOptions"
        />
        <DxSimpleItem data-field="FirstName">
          <DxRequiredRule/>
          <DxStringLengthRule :max="10"/>
        </DxSimpleItem>
        <DxSimpleItem data-field="LastName">
          <DxRequiredRule/>
          <DxStringLengthRule :max="10"/>
        </DxSimpleItem>
        <DxSimpleItem
          data-field="HireDate"
          editor-type="dxDateBox"
        />
      </DxForm>
      <DxDataGrid
        id="grid"
        ref="gridRef"
        :data-source="orders"
        key-expr="OrderID"
        :show-borders="true"
        date-serialization-format="MM-dd-yyyy"
        :remote-operations="false"
      >
        <DxEditing
          :allow-updating="true"
          :allow-adding="true"
          :allow-deleting="true"
        />
        <DxPaging :page-size="10"/>
        <DxFilterRow :visible="true"/>
        <DxHeaderFilter :visible="true"/>
        <DxGroupPanel :visible="true"/>
        <DxGrouping :auto-expand-all="false"/>
        <DxColumn data-field="OrderID"/>
        <DxColumn
          data-field="OrderDate"
          data-type="date"
        />
        <DxColumn data-field="CustomerName"/>
        <DxColumn data-field="ShipCountry"/>
        <DxColumn data-field="ShipCity"/>
      </DxDataGrid>
      <DxButton
        text="Validate and Submit"
        validation-group="customer"
        :use-submit-behavior="false"
        @click="onButtonClick"
      />
    </form>
    <pre id="payload">{{ payload }}</pre>
  </div>
</template>
<style>
#grid {
  margin: 20px 0;
}

#payload {
  margin-top: 20px;
  padding: 20px;
  background-color: rgb(191 191 191 / 15%);
}
</style>
