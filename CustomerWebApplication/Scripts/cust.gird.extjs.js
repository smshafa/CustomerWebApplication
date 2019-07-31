Ext.require(['Ext.data.*', 'Ext.grid.*']);

//Ext.define('Person', {
//    extend: 'Ext.data.Model',
//    fields: [{
//        name: 'id',
//        type: 'int',
//        useNull: true
//    }, 'email', 'first', 'last'],
//    validations: [{
//        type: 'length',
//        field: 'email',
//        min: 1
//    }, {
//        type: 'length',
//        field: 'first',
//        min: 1
//    }, {
//        type: 'length',
//        field: 'last',
//        min: 1
//    }]
//});


Ext.define('Customer', {
    extend: 'Ext.data.Model',
    idProperty: 'customerId',
    fields: [{
        name: 'CustomerID',
        type: 'int',
        //useNull: true
    }, 'FirstName', 'LastName', 'CityName', 'ProvinceName'],
    validations: [ {
        type: 'length',
        field: 'FirstName',
        min: 1
    }, {
        type: 'length',
        field: 'LastName',
        min: 1
    }]
});


Ext.onReady(function () {

    var store = Ext.create('Ext.data.Store', {        
        autoLoad: true,
        autoSync: true,
        model: 'Customer',
        //proxy: {
        //    type: 'rest',
        //    url: '/CustomerExtJS/Load',
        //    reader: {
        //        type: 'json',
        //        rootProperty: 'data',
        //        successProperty: 'success',
        //        root: 'data',
        //        messageProperty: 'message'
        //    },
        //    writer: {
        //        type: 'json'
        //    }
        //},
        proxy: {
            type: 'ajax',
            //type: 'rest',
            //type: 'json',
            api: {
                read: '/CustomerExtJS/Load',
                create: '/CustomerExtJS/Create',
                update: '/CustomerExtJS/Update',
                destroy: '/CustomerExtJS/Delete'
            },
            reader: {
                type: 'json',
                successProperty: 'success',
                root: 'data',
                messageProperty: 'message'
            },
            writer: {
                type: 'json',   //which data type server will be worked
                writeAllFields: true,   //true to write all fields from the record to the server. If set to false it will only send the fields that were modified.
                root: 'data',                
                encode: true,                
                rootProperty: 'data',
                writeRecordId: false //By default, each record's id is always included in the output for non-phantom records since in most cases the id will be required on the server to process th
            }
        },      
        listeners: {
            write: function (store, operation) {
                var record = operation.getRecords()[0],
                    name = Ext.String.capitalize(operation.action),
                    verb;


                //if (name == 'Destroy') {
                if (name == 'Delete') {
                    verb = 'Destroyed';
                } else {
                    verb = name + 'd';
                }
                Ext.example.msg(name, Ext.String.format("{0} customer: {1}", verb, record.getId()));

            }
        }
    });
    

    var grid = Ext.create('Ext.grid.Panel', {
        renderTo: document.body,
        plugins: {
            rowediting: {
                listeners: {
                    cancelEdit: function (rowEditing, context) {
                        // Canceling editing of a locally added, unsaved record: remove it
                        if (context.record.phantom) {
                            store.remove(context.record);
                        }
                    }
                }
            }
        },
        width: 800,
        height: 330,
        frame: true,
        title: 'Customers',
        store: store,
        iconCls: 'icon-user',
        columns: [{
            text: 'ID',
            width: 50,
            sortable: true,
            dataIndex: 'CustomerID',
            renderer: function (v, meta, rec) {                
                return rec.phantom ? '' : v;
            }
        }, {
            header: 'First Name',
            width: 120,
            sortable: true,
            dataIndex: 'FirstName',
            field: {
                xtype: 'textfield'
            }
        }, {
            text: 'Last Name',
            width: 120,
            sortable: true,
            dataIndex: 'LastName',
            field: {
                xtype: 'textfield'
            }
        }, {
            text: 'City',
            width: 120,
            sortable: true,
            dataIndex: 'CityName',
            field: {
                xtype: 'textfield'
            }
        }, {
            text: 'Province',
            width: 120,
            sortable: true,
            dataIndex: 'ProvinceName',
            field: {
                xtype: 'textfield'
            }
        }],
        dockedItems: [{
            xtype: 'toolbar',
            items: [{
                text: 'Add',
                iconCls: 'icon-add',
                handler: function () {
                    // empty record
                    var rec = new Customer();
                    var rowEditing = grid.findPlugin('rowediting');
                    store.insert(0, rec);
                    rowEditing.startEdit(rec, 0);
                }
            }, '-', {
                itemId: 'delete',
                text: 'Delete',
                iconCls: 'icon-delete',
                disabled: true,
                handler: function () {
                    var selection = grid.getView().getSelectionModel().getSelection()[0];
                    if (selection) {
                        store.remove(selection);
                    }
                }
            }]
        }]
    });
    grid.getSelectionModel().on('selectionchange', function (selModel, selections) {
        grid.down('#delete').setDisabled(selections.length === 0);
    });
});
