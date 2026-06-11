$(() => {
  const customer = {
    CustomerID: 1,
    FirstName: '',
    LastName: '',
    HireDate: null,
  };

  $('#form').dxForm({
    formData: customer,
    validationGroup: 'customer',
    items: [
      {
        dataField: 'CustomerID',
        editorType: 'dxNumberBox',
        editorOptions: {
          readOnly: true,
        },
      },
      {
        dataField: 'FirstName',
        isRequired: true,
        validationRules: [{ type: 'stringLength', max: 10 }],
      },
      {
        dataField: 'LastName',
        isRequired: true,
        validationRules: [{ type: 'stringLength', max: 10 }],
      },
      {
        dataField: 'HireDate',
        editorType: 'dxDateBox',
      },
    ],
  });

  const grid = $('#grid').dxDataGrid({
    dataSource: [],
    keyExpr: 'OrderID',
    showBorders: true,
    editing: {
      allowUpdating: true,
      allowAdding: true,
      allowDeleting: true,
    },
    dateSerializationFormat: 'MM-dd-yyyy',
    columns: ['OrderID',
      {
        dataField: 'OrderDate',
        dataType: 'date',
      },
      'CustomerName',
      'ShipCountry',
      'ShipCity',
    ],
    paging: {
      pageSize: 10,
    },
    filterRow: {
      visible: true,
    },
    headerFilter: {
      visible: true,
    },
    groupPanel: {
      visible: true,
    },
    grouping: {
      autoExpandAll: false,
    },
    remoteOperations: false,
  }).dxDataGrid('instance');

  const createInputElement = (itemName, itemValue, itemIndex, container) => {
    $('<input/>')
      .appendTo(container)
      .addClass('order-input')
      .attr({ type: 'hidden', name: `Orders[${itemIndex}].${itemName}` })
      .val(itemValue);
  };

  $('#edit-employee').on('submit', (e) => {
    e.preventDefault();
    const payload = decodeURIComponent($('#edit-employee').serialize()).split('&').join('\n');
    $('#payload').text(`Submitted payload:\n${payload}`);
  });

  $('#submit-button').dxButton({
    text: 'Validate and Submit',
    validationGroup: 'customer',
    useSubmitBehavior: false,
    onClick(e) {
      if (!e.validationGroup.validate().isValid) {
        return;
      }
      const formElement = $('#edit-employee');
      formElement.find('input.order-input').remove();
      grid.getDataSource().load().done((items) => {
        items.forEach((item, itemIndex) => {
          Object.keys(item).forEach((property) => {
            createInputElement(property, item[property], itemIndex, formElement[0]);
          });
        });
        formElement.trigger('submit');
      });
    },
  });
});
