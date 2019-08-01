﻿Ext.require(['Ext.data.*', 'Ext.grid.*']);

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

//Asus
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

                if (name == 'Create') {
                    alert('Created.');                    
                    //store.reload.refresh();
                }
                if (name == 'Update') {
                    alert('update');
                    //store.reload();
                    //grid.store.reload();
                }

            }
        }
    });


    Ext.define('ProvinceModel', {
        extend: 'Ext.data.Model',
        fields: [{
                name: 'ProvinceID',
                type: 'int'
            }, {
                name: 'ProvinceName',
                type: 'string'
            },

        ]
    });

    var province = Ext.create('Ext.data.Store', {
        //url: '/CustomerExtJS/City',
        //root: 'data',
        //fields: ['CityID', 'CityName'],
        model: 'ProvinceModel',
        //data:
        autoLoad: false,
        proxy: {
            type: 'ajax',
            url: '/CustomerExtJS/Province',
            reader: {
                type: 'json',
                rootProperty: 'data'
            }
        },
        
    });

    

    // The data store containing the list of states
    // static mode
    //var city = Ext.create('Ext.data.Store', {
    //    fields: ['abbr', 'name'],
    //    data: [
    //        { "abbr": "AL", "name": "Alabama" },
    //        { "abbr": "AK", "name": "Alaska" },
    //        { "abbr": "AZ", "name": "Arizona" }
    //        //...
    //    ]
    //});

    // Set up a Ext.data.Model to use in our Store
    //Ext.define('CityModel', {
    //    extend: 'Ext.data.Model',
    //    fields: ['CityID', 'CityName']
    //    //fields: [
    //    //    { name: 'firstName', type: 'string' },
    //    //    { name: 'lastName', type: 'string' },
    //    //    { name: 'age', type: 'int' },
    //    //    { name: 'eyeColor', type: 'string' }
    //    //]
    //});

    Ext.define('CityModel', {
        extend: 'Ext.data.Model',
        fields: [{
            name: 'CityID',
            type: 'int'
        }, {
            name: 'CityName',
            type: 'string'
        },

        ]
    });

    var city = Ext.create('Ext.data.Store', {
        //url: '/CustomerExtJS/City',
        //root: 'data',
        //fields: ['CityID', 'CityName'],
        model: 'CityModel',
        //data:
        proxy: {
            type: 'ajax',
            url: '/CustomerExtJS/City',
            reader: {
                type: 'json',
                rootProperty: 'data'
            }
        },
        //autoLoad: true
    });

    //http://abdelraoof.com/blog/2011/01/28/cascading-combo-box-in-ext-js/
    var cityStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        model: 'CityModel',
        pruneModifiedRecords: true,
        //type: 'ajax',
        //root: 'data',
        //url: '/CustomerExtJS/GetCity',
        //fields: ['CityID', 'CityName'],
        proxy: {
            type: 'ajax',
            url: '/CustomerExtJS/GetCity',
            reader: {
                type: 'json',
                rootProperty: 'data'
            }
        }
    });
    

    //myGrid.store.reload();
    //myGrid.getView().refresh();

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
                    },
                    edit: function(editor, e) {
                        // commit the changes right after editing finished                        
                        e.record.commit();
                        store.add(e);
                        alert('hi');

                        grid.store.reload();
                        //Ext.getCmp('crud-grid').getView().refresh();
                        //grid.getView().refresh();                        

                        //this.store.refresh();
                        //this.getView().refresh();
                        //grid.getView().refresh();
                    }
                }
            }
        },
        width: 800,
        height: 330,
        renderTo: 'crud-grid',
        style: 'margin-top: 10px',
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
                //}, {
                //    text: 'City',
                //    width: 120,
                //    sortable: true,
                //    dataIndex: 'CityName',
                //    field: {
                //        xtype: 'textfield'
                //    }
            }, {
                header: 'Province',
                dataIndex: 'ProvinceName',
                editor: {
                    xtype: 'combobox',
                    store: province, // It is a function
                    displayField: 'ProvinceName',
                    valueField: 'ProvinceName',
                    typeAhead: true,
                    //queryMode: 'local',
                    hiddenName: 'Province',
                    //triggerAction: 'all',
                    //id: 'province',
                    listeners: {
                        'select': function (combo, records) {
                            //alert(records[0].get('descricao'));
                            //var p = records[0].get('descricao');

                            //value = this.getValue();
                            //var record = this.findRecordByValue(value);
                            //var index = this.getStore().indexOf(record);

                            //alert(combo.getValue());
 
                            cityStore.load({
                                params: { 'province': combo.getValue() }
                            });
                        }
                    }
                    //forceSelection: true,
                    //triggerAction: 'all'
                }
            }, {
                header: 'City',
                dataIndex: 'CityName',
                editor: {
                    xtype: 'combobox',
                    //store: city, // It is a function***
                    store: cityStore,
                    displayField: 'CityName',
                    valueField: 'CityName',
                    typeAhead: true,
                    queryMode: 'local', //https://forums.ext.net/showthread.php?20126-CLOSED-Store-loads-twice
                    forceSelection: true,
                    //triggerAction: 'all'
                    editable: false,
                    allowBlank: false,
                    emptyText: 'Select a City',
                    // static mode
                    //store: city, // It is a function
                    //displayField: 'name',
                    //valueField: 'abbr',

                    //other mode
                    //    store: Ext.create('Ext.data.Store', {
                    //        fields: ['id', 'name', 'mydata'],
                    //        data: [
                    //            { 'id': '1', 'name': 'John Smith', 'mydata': ["3", "4"] },
                    //            { 'id': '2', 'name': 'Albert Einstein', 'mydata': ["1", "2"] }
                    //        ]
                    //    }),
                    //    listeners: {
                    //        select: function (combo, records, eOpts) {
                    //            alert(records[0].get('mydata')); //

                    //            //Using JQuery Ajax to call the relevant method
                    //        }
                    //    }
                    //}
                
                

                }
        //}, {
        //    text: 'Province',
        //    width: 120,
        //    sortable: true,
        //    dataIndex: 'ProvinceName',
        //    field: {
        //        xtype: 'textfield'
        //    }
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

                    //Ext.getCmp('crud-grid').getView().refresh();
                    //neww
                    //editor.stopEditing();

                    //grid.getView().refresh();
                    //grid.getSelectionModel().selectRow(0);
                    //rowEditing.startEditing(0);
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
                }, '-', {
                    itemId: 'refresh',
                    text: 'Refresh',
                    iconCls: 'icon-delete',
                    disabled: false,
                    handler: function () {
                        grid.store.refresh();
                        grid.getView().getSelectionModel().selectRow(0);
                        grid.getView().reload();

                    }
                }
            ]
        }]
    });
    grid.getSelectionModel().on('selectionchange', function (selModel, selections) {
        grid.down('#delete').setDisabled(selections.length === 0);
    });
});


//https://fiddle.sencha.com/#fiddle/ob5&view/editor
//https://fiddle.sencha.com/#fiddle/s5f&view/editor
 //   https://fiddle.sencha.com/#view/editor&fiddle/2f6d


//Example:
//Ext.define('Customer', {
//    extend: 'Ext.data.Model',

//    config: {
//        fields: [
//            'text',
//            'value'
//        ]
//    }
//});

//var CustomerStore = Ext.create('Ext.data.Store', {
//    autoLoad: true,
//    model: 'Customer',
//    proxy: {
//        type: 'ajax',
//        url: 'data/json.json',
//        reader: {
//            type: 'json',
//            rootProperty: 'data'
//        }
//    },
//    listeners: {
//        load: function (store, records) {
//            console.log(records.length);
//        }
//    }
//});

// data / json.json :
//{
//    "data" : [
//        {
//            "text": "CustOneFromC#",
//            "value": "1"
//        },
//        {
//            "text": "CustTwoFromC#",
//            "value": "2"
//        }
//    ]
//}