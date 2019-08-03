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

//Asus
Ext.define('Customer',
    {
        extend: 'Ext.data.Model',
        idProperty: 'customerId',
        fields: [
            {
                name: 'CustomerID',
                type: 'int',
                //useNull: true
            }, 'FirstName', 'LastName', 'CityName', 'ProvinceName'
        ],
        validations: [
            //{
            //    type: 'length',
            //    field: 'FirstName',
            //    min: 1,
            //    max: 60
            //}, {
            {
                type: 'length',
                field: 'LastName',
                min: 1,
                max: 60
            }, {
                type: 'notnull',
                field: 'FirstName',
                message: 'not null'
            }
        ]
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
                writeRecordId: false, //By default, each record's id is always included in the output for non-phantom records since in most cases the id will be required on the server to process th
                successProperty: 'success',
                messageProperty: 'message'
            }
        },      
        listeners: {
            load: function(store, record, success, opts) {
                var response_text = store.proxy.reader.rawData;
                console.log(response_text);
                var data = Ext.JSON.decode(action.response.responseText);
                alert(response_text);
                //alert(success);
            },
            // not work
            //load: function (records, operation, success) {
            //    if (success) {
            //        console.log("Category: " + category.get('name'));
            //        alert('s');
            //    } else {
            //        console.log('error');
            //        alert('f');
            //    }
            //},
            write: function (records, operation, success) {
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
                    a
                    //Ext.example.msg(name, Ext.String.format("مشتری {1} اضافه گردید.", verb, record.getId()));
                }
                if (name == 'Update') {
                    alert('update');
                    
                    grid.store.reload();
                    //Ext.example.msg(name, Ext.String.format("مشتری {1} بروز گردید.", verb, record.getId()));
                }
                
                //},
                //load: function (store, records, successfull, eOpts) {
                //    if (successfull) {
                //        alert('success');
                //    }
                //}
            }
            //, update: function (thisStore, record, operation) {
            //    console.log('Update happened');
            //    console.log(record);
            //    console.log(operation);
            //    alert('Update happened');
            //    if (operation.wasSuccessful()) {
            //        alert('Update dfd');
            //    } else {
            //        alert('Update dsfdsfd');
            //    }
            //},
            //save: function () {
            //    console.log('Save happened');
            //    alert('Save happened');
            //},
            //exception: function (dataproxy, type, action, options, response, arg) {
            //    alert('Error happened');
            //    console.log('Error happened');
            //    console.log(response);
            //    doJSON(result.responseText);
            //},
            //remove: function () {
            //    console.log("Record removed");
            //    alert("Record removed");
            //}
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

    //myGrid.store.reload();
    //myGrid.getView().refresh();

    //https://docs.sencha.com/extjs/6.0.2/classic/Ext.plugin.Viewport.html
    //Ext.create('Ext.container.Viewport', {
    //    layout: 'fit', // full the viewport with the tab panel

    //    items: [{
    //        xtype: 'panel'//,
    //        items: [{
    //            ...
    //     }]
    //    }]
    //});

    //Ext.create('Ext.tab.Panel', {
    //    plugins: 'viewport',

    //    items: [{
    //        ...
    // }]
    //});



    //https://docs.sencha.com/extjs/6.0.2/classic/Ext.container.Viewport.html
    //Ext.create('Ext.container.Viewport', {
    //    layout: 'border',
    //    renderTo: Ext.getBody(),
    //    rtl: true,
    //    items: [
            //{
            //    region: 'north',
            //    html: '<h1 class="x-panel-header">Page Title</h1>',
            //    border: false,
            //    margin: '0 0 5 0'
            //}//, {
            //    region: 'west',
            //    collapsible: true,
            //    title: 'Navigation',
            //    width: 150
            //    // could use a TreePanel or AccordionLayout for navigational items
            //}, {
            //    region: 'south',
            //    title: 'South Panel',
            //    collapsible: true,
            //    html: 'Information goes here',
            //    split: true,
            //    height: 100,
            //    minHeight: 100
            //}, {
            //    region: 'east',
            //    title: 'East Panel',
            //    collapsible: true,
            //    split: true,
            //    width: 150
            //},
            //,{
            //    region: 'center',
            //    xtype: 'tabpanel', // TabPanel itself has no title
            //    activeTab: 0,      // First tab active by default
            //    items: {
            //        title: 'Default Tab',
            //        //html: 'The first tab\'s content. Others may be added dynamically'
            //    }
            //}


    //        {
    //            region: 'center',
    //            border: false,
    //            layout: 'fit'               
    //        }
    //    ]
    //});


    

    

    var grid = Ext.create('Ext.grid.Panel', {
        renderTo: document.body,
        plugins: {
            //https://docs.sencha.com/extjs/6.0.1/classic/Ext.grid.plugin.RowEditing.html#cfg-errorSummary
            rowediting: {
                clicksToEdit: 1,
                clicksToMoveEditor: 1,
                errorSummary: true,
                listeners: {
                    cancelEdit: function (rowEditing, context) {
                        // Canceling editing of a locally added, unsaved record: remove it
                        if (context.record.phantom) {
                            store.remove(context.record);
                        }
                    },
                    edit: function (editor, e) {                        
                        // commit the changes right after editing finished                        
                        e.record.commit();
                        store.add(e);                        
                                              
                        grid.store.reload();                        
                    }//,
                    //validateedit(editor, context, eOpts) {
                        //https://docs.sencha.com/extjs/6.0.1/classic/Ext.grid.plugin.RowEditing.html#method-cancelEdit
                        //var currentRec = context.record.data;
                        ////alert(currentRec.FirstName);                                           
                    //}

                }
            }
        },
        // *** if viewport doesn't need, set width and height:
        //width: 800,
        //height: 330,

        //plugins: 'viewport', // add this component to the viewport container.

        // *** if viewport doesn't need, set which container of html page should keep the grid:
        //renderTo: 'crud-grid',

        // ******configuration of  view-port: https://www.sencha.com/forum/showthread.php?163309-Ext-js-4-Viewport-implementation
        autoScroll: true,
        scrollable: true,
        collapsible: true,
        region: 'center',
        //region: 'west',
        region: 'east',
        split: true,
        // if you want to the grid fit to the page's width, delete this property.
        //width: 700,
        rtl: true,
        //**********
        
        //style: 'margin-top: 10px',
        frame: true,
        title: 'مشتریان',
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
                header: 'نام',
                width: 220,
                sortable: true,
                dataIndex: 'FirstName',
                // Hier you can put make your validation
                //renderer: function (value, metaData) {
                //    //console.log(arguments);                
                //    var patt = /[0-9]/g;
                //    var result = value.match(patt);
                //    //console.log(result);
                //    if (result != null) {
                //        metaData.innerCls = 'invalidCell';
                //        metaData.tdAttr = 'data-qtip="لطفا از ورود عدد در ورودی نام خودداری فرمایید."';
                //    }
                //    return value;
                //},
                field: {
                    xtype: 'textfield'
                }
            }, {
                text: 'نام خانوادگی',
                width: 220,
                sortable: true,
                dataIndex: 'LastName',
                //field: {
                //    xtype: 'textfield'
                //},
                editor: {
                    xtype: 'textfield',
                    allowBlank: false,
                    validator: function (value) {
                        //return (value > 5 && value < 100) || 'Number must be > 5 and < 100';
                        
                        var patt = /[0-9]/g;
                        var result = value.match(patt);                        
                        if (result != null) {                           
                            return 'لطفا اعداد در نام خانوادگی وارد نکنید.';
                        }
                        return true;
                    }
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
                header: 'استان',
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
                header: 'شهر',
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
            style: 'margin-top: 20px',
            items: [{
                text: 'اضافه کردن',
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
                text: 'حذف',
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
                    text: 'تازه‌سازی',
                    iconCls: 'icon-delete',
                    disabled: false,
                    handler: function () {
                        grid.store.reload();
                        //grid.store.refresh();
                        //grid.getView().getSelectionModel().selectRow(0);
                        //grid.getView().reload();

                    }
                }
            ]
        }]
    });
    grid.getSelectionModel().on('selectionchange', function (selModel, selections) {
        grid.down('#delete').setDisabled(selections.length === 0);
    });   


    var tree = Ext.create('Ext.tree.Panel', {
        collapsible: true,
        region: 'west',
        rootVisible: false,
        store: store,
        title: 'Book Details Tree',
        width: 200
    });



    Ext.create('Ext.container.Viewport',
        {
            //autoScroll: true,
            scrollable: true,
            //layout: 'border',
            //xtype: 'panel', // This your UserGrid
            renderTo: Ext.getBody(),
            //margin: '100 100 100 100',
            layout: 'fit',
            //region: 'center',
            rtl: true,
            title: 'East Panel',
            //items: [{
            //    scrollable: true,
            //    html: grid
            //}]
            items: [grid, tree]
        }
    );

    //// *** it works: *********
    //Ext.create('Ext.container.Viewport',
    //    {
    //        layout: 'border',
    //        renderTo: Ext.getBody(),
    //        rtl: true,
    //        //items: [tree, grid]
    //        items: [grid]
    //    }
    //);


    //Ext.create('Ext.container.Viewport',
    //    {
    //        layout: 'border',
    //        renderTo: Ext.getBody(),
    //        rtl: true,
    //        items:
    //            [
    //                {
    //                    grid,
    //                    //layout: 'border',
    //                    //xtype: 'panel', // This your UserGrid
    //                    //renderTo: Ext.getBody(),
    //                    //margin: '100 100 100 100',
    //                    //layout: 'fit',
    //                    region: 'center',
    //                    //html: "<div id='crud-grid'></div>",
    //                    rtl: true,
    //                    title: 'East Panel',
    //                    //html: "hello I am a bunch of text"
    //                    //html: grid,//'<h1 class="x-panel-header">Page Title</h1>',
    //                    border: false,
    //                },
    //                , {
    //                    region: 'west',
    //                    collapsible: true,
    //                    title: 'Navigation',
    //                    width: 150,
    //                    html: 'jsdfkjssdfjksdjfk'
    //                    // could use a TreePanel or AccordionLayout for navigational items
    //                }
    //            ]
    //    }
    //);

    //var rowEditing = grid.findPlugin('rowediting');
    //rowEditing.startEdit(0, 0);
});



//****** sample(asus) these blew code does not related to the work. they are jsut sample code.
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