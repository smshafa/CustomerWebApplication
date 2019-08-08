Ext.require(['Ext.data.*', 'Ext.grid.*', 'Ext.app.*']);



Ext.onReady(function () {

    var grid = Ext.create('Ext.grid.Panel', {
        //renderTo: document.body,  //  which container is charge of keeping this component.
        plugins: {
            //https://docs.sencha.com/extjs/6.0.1/classic/Ext.grid.plugin.RowEditing.html#cfg-errorSummary
            rowediting: {
                clicksToEdit: 2,
                clicksToMoveEditor: 2,
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

        // ******configuration of  view-port: https://www.sencha.com/forum/showthread.php?163309-Ext-js-4-Viewport-implementation
        autoScroll: true,
        scrollable: true,
        collapsible: true,
        region: 'center',
        //region: 'west',
        autoHeight: true,
        //region: 'east',
        split: true,
        // if you want to the grid fit to the page's width, delete this property.
        //width: 700,
        rtl: true,
        //**********

        //style: 'margin-top: 10px',
        frame: true,
        title: 'مشتریان',

        // ***** uses this property if you are using Ext.data.Store directly
        //store: store,   
        // *****


        // ***** uses this property if you are using Ext.app.ViewModel directly
        viewModel: { type: 'customerviewmodel' },
        bind: {
            store: '{CustomerListStore}'
        },
        // ***** 

        iconCls: 'icon-user',
        columns: [{
            text: 'ID',
            width: 50,
            sortable: true,
            dataIndex: 'CustomerID',
            renderer: function (v, meta, rec) {
                return rec.phantom ? '' : v;
            },

            //bind: {
            //    value: '{CustomerID}',
            //    dataIndex: '{CustomerID}'
            //}
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
            editor: {
                xtype: 'textfield',
                allowBlank: false,
                validator: function (value) {
                    var patt = /[0-9]/g;
                    var result = value.match(patt);
                    if (result != null) {
                        return 'لطفا اعداد در نام خانوادگی وارد نکنید.';
                    }
                    return true;
                }
            }
        }, {
            header: 'استان',
            width: 220,
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
                        cityStore.load({
                            params: { 'province': combo.getValue() }
                        });
                    }
                }
            }
        }, {
            header: 'شهر',
            dataIndex: 'CityName',
            width: 220,
            editor: {
                xtype: 'combobox',
                //store: city, // It is a function***
                store: cityStore,
                displayField: 'CityName',
                valueField: 'CityName',
                typeAhead: true,
                queryMode: 'local', //https://forums.ext.net/showthread.php?20126-CLOSED-Store-loads-twice
                forceSelection: true,
                editable: false,
                allowBlank: false,
                emptyText: 'Select a City',
            }
        }],
        dockedItems: [{
            xtype: 'toolbar',
            //style: 'margin-top: 20px',
            items: [{
                text: 'اضافه کردن',
                //iconCls: 'icon-user-add',//'icon-add',
                handler: function () {
                    // empty record
                    var rec = new Customer();


                    var rowEditing = grid.findPlugin('rowediting');

                    // For Ext.data.Store
                    //store.insert(0, rec);

                    // For Ext.app.ViewModel
                    grid.store.insert(0, rec);

                    rowEditing.startEdit(rec, 0);
                }
            }, '-', {
                itemId: 'delete',
                text: 'حذف',
                iconCls: 'icon-delete',
                disabled: true,
                handler: function () {
                    var selection = grid.getView().getSelectionModel().getSelection()[0];
                    if (selection) {

                        // For Ext.data.Store
                        //store.remove(selection);

                        // For Ext.app.ViewModel
                        grid.store.remove(selection);
                    }
                }
            }, '-', {
                itemId: 'refresh',
                text: 'تازه‌سازی',
                iconCls: 'icon-delete',
                disabled: false,
                handler: function () {
                    grid.store.reload();
                }
            }
            ]
        }]
    });
    grid.getSelectionModel().on('selectionchange', function (selModel, selections) {
        grid.down('#delete').setDisabled(selections.length === 0);
    });

    Ext.create('Ext.container.Viewport',
        {
            //el: 'Form1',
            //autoScroll: true,
            scrollable: true,
            //layout: 'border',            
            renderTo: Ext.getBody(),
            //margin: '100 100 100 100',
            layout: 'fit',
            //region: 'center',
            rtl: true,
            title: 'East Panel',
            padding: '0 0 40 0',
            //items: [{
            //    scrollable: true,
            //    html: grid
            //}]
            //https://www.sencha.com/forum/showthread.php?37138-Viewport-inside-form-element-insted-of-body-element/page2&s=d6724e0b89e282d745e196985bec8a5c
            items: [
                {
                    //el: 'ID_OF_MY_FORM_INSIDE_BODY',  // instead of body
                    xtype: 'container',
                    layout: 'border',
                    margin: '50 0 0 0',
                    items: [grid]
                }
            ]
        }
    );
});