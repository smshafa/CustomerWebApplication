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
    validations: [{
        type: 'length',
        field: 'FirstName',
        min: 1,
        max: 60
    }
        , {
        type: 'length',
        field: 'LastName',
        min: 1,
        max: 60
    }
        , {
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
                    //Ext.example.msg(name, Ext.String.format("مشتری {1} اضافه گردید.", verb, record.getId()));
                }
                if (name == 'Update') {
                    alert('update');                    
                    //Ext.example.msg(name, Ext.String.format("مشتری {1} بروز گردید.", verb, record.getId()));
                }

            }
        }
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
        proxy: {
            type: 'ajax',
            url: '/CustomerExtJS/Province',
            reader: {
                type: 'json',
                rootProperty: 'data'
            }
        },
        autoLoad: true
    });

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
            rowediting: {
                clicksToEdit: 1,
                clicksToMoveEditor: 1,
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
                    }
                }
            }
        },
        // *** if viewport doesn't need, set width and height:
        //width: 800,
        //height: 330,

        //plugins: 'viewport', // add this component to the viewport container.

        // *** if viewport doesn't need, set which container of html page should keep the grid:
        //renderTo: 'crud-grid',

        // ******configuration of  view-port:
        autoScroll: true,
        collapsible: true,
        //region: 'east',
        region: 'west',
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
            width: 120,
            sortable: true,
            dataIndex: 'FirstName',
            field: {
                xtype: 'textfield'
            }
        }, {
            text: 'نام خانوادگی',
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
                header: 'شهر',
                dataIndex: 'CityName',
                editor: {
                    xtype: 'combobox',
                    store: city, // It is a function
                    displayField: 'CityName',
                    valueField: 'CityName',
                    typeAhead: true,
                    mode: 'remote',
                    //forceSelection: true,
                    //triggerAction: 'all'

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
            }, {
                header: 'استان',
                dataIndex: 'ProvinceName',
                editor: {
                    xtype: 'combobox',
                    store: province, // It is a function
                    displayField: 'ProvinceName',
                    valueField: 'ProvinceName',
                    typeAhead: true,
                    mode: 'remote',
                    //forceSelection: true,
                    //triggerAction: 'all'

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
