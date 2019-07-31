
if ((typeof Range !== "undefined") && !Range.prototype.createContextualFragment) {
    Range.prototype.createContextualFragment = function(html) {
        var div = document.createElement("div"),
            fragment = document.createDocumentFragment(),
            i = 0,
            length,
            childNodes;

        div.innerHTML = html;
        childNodes = div.childNodes;
        length = childNodes.length;

        for (; i < length; i++) {
            fragment.appendChild(childNodes[i].cloneNode(true));
        }

        return fragment;
    };
}

if (Ext.BLANK_IMAGE_URL.substr(0, 5) != 'data:') {
    Ext.BLANK_IMAGE_URL = '/Lib/ext-3.3.1/resources/images/default/s.gif';
}


Ext.onReady(function () {
    alert('test');

    Ext.QuickTips.init();

    var customers = Ext.data.Record.create([
        {
            name: 'CustomerID',
            type: 'int'
        }, {
            name: 'FirstName',
            type: 'string'
        }, {
            name: 'LastName',
            type: 'string'
        }, {
            name: 'ProvinceName',
            type: 'string'
        }, {
            name: 'CityName',
            type: 'string'
        }
    ]);

    var writer = new Ext.data.JsonWriter({
        encode: false,
        listful: true,
        writeAllFields: true
    });

    var reader = new Ext.data.JsonReader({
            totalProperty: 'total',
            successProperty: 'success',
            idProperty: 'CustomerID',
            root: 'data',
            messageProperty: 'message' // <-- New "messageProperty" meta-data
        },
        customers);

    var proxy = new Ext.data.HttpProxy({
        api: {
            read: '/CustomerExtJS/Load',
            create: '/CustomerExtJS/Create',
            update: '/CustomerExtJS/Update',
            destroy: '/CustomerExtJS/Delete'
        },
        headers: { 'Content-Type': 'application/json; charset=UTF-8' }
    });

    var store = new Ext.data.Store({
        id: 'customer',
        proxy: proxy,
        reader: reader,
        writer: writer, // <-- plug a DataWriter into the store just as you would a Reader
        autoSave:
            false // <-- false would delay executing create, update, destroy requests until specifically told to do so with some [save] buton.
    });

    store.load({ params: Ext.util.JSON.encode({ start: 0, limit: 2 }) });

    //Ext.data.DataProxy.addListener('exception',
    //    function(proxy, type, action, options, res) {
    //        Ext.Msg.show({
    //            title: 'ERROR',
    //            msg: res.message,
    //            icon: Ext.MessageBox.ERROR,
    //            buttons: Ext.Msg.OK
    //        });
    //    });

    //var editor = new Ext.ux.grid.RowEditor({
    //    saveText: 'Update'
    //});

    var grid = new Ext.grid.GridPanel({
        store: store,
        columns: [
            {
                header: "Customer ID",
                width: 170,
                sortable: true,
                dataIndex: 'CustomerID'//,
                //editor: {
                //    xtype: 'textfield',
                //    allowBlank: false
                //}
            },
            {
                header: "Name",
                width: 160,
                sortable: true,
                dataIndex: 'FirstName'//,
                //editor: {
                //    xtype: 'textfield',
                //    allowBlank: false
                //}
            },
            {
                header: "Last Name",
                width: 170,
                sortable: true,
                dataIndex: 'LastName'//,
                //editor: {
                //    xtype: 'textfield',
                //    allowBlank: false,
                //    //vtype: 'email'
                //}
            },
            {
                //xtype: 'datecolumn',
                header: "Province",
                width: 170,
                sortable: true,
                dataIndex: 'ProvinceName'//,
                //format: 'j/n/Y',
                //editor: {
                //    //xtype: 'datefield',
                //    xtype: 'textfield',
                //    allowBlank: false
                //}
            },
            //{
            //    xtype: 'booleancolumn',
            //    header: "Is Married?",
            //    width: 170,
            //    align: 'center',
            //    sortable: false,
            //    trueText: 'Yes',
            //    falseText: 'No',
            //    dataIndex: 'IsMarried',
            //    editor: {
            //        xtype: 'checkbox'
            //    }
            //},
            {
                header: "City",
                width: 170,
                sortable: false,
                dataIndex: 'CityName'//,
                //editor: {
                //    //xtype: 'numberfield',
                //    xtype: 'textfield',
                //    allowBlank: false
                //}
            }
        ]//,
        //plugins: [editor],
        //title: 'Customer DataGrid',
        //height: 300,
        //width: 1010,
        //tbar: [
        //    {
        //        iconCls: 'icon-user-add',
        //        text: 'Add Customer',
        //        handler: function() {
        //            var e = new customers({
        //                CustomerID: '',
        //                FirstName: 'Hamid',
        //                LastName: 'Shafaei',
        //                //BirthDate: new Date(),
        //                //IsMarried: false,
        //                //NoOfCar: 0
        //                ProvinceName : "Hamedan",
        //                CityName : "Hamedan"
        //            });
        //            editor.stopEditing();
        //            store.insert(0, e);
        //            grid.getView().refresh();
        //            grid.getSelectionModel().selectRow(0);
        //            editor.startEditing(0);
        //        }
        //    }, {
        //        ref: '../removeBtn',
        //        iconCls: 'icon-user-delete',
        //        text: 'Remove Customer',
        //        handler: function() {
        //            editor.stopEditing();
        //            var s = grid.getSelectionModel().getSelections();
        //            for (var i = 0, r; r = s[i]; i++) {
        //                store.remove(r);
        //            }
        //        }
        //    }, {
        //        iconCls: 'icon-user-save',
        //        text: 'Save All Modifications',
        //        handler: function() {
        //            store.save();
        //        }
        //    }
        //],
        
// paging bar on the bottom
        //bbar: new Ext.ux.JsonPagingToolbar({
        //    pageSize: 2,
        //    store: store,
        //    displayInfo: true,
        //    displayMsg: 'Displaying topics {0} - {1} of {2}',
        //    emptyMsg: "No customers to display"
        //})
    });

    grid.getSelectionModel().on('selectionchange',
        function(sm) {
            grid.removeBtn.setDisabled(sm.getCount() < 1);
        });

    grid.render('crud-grid');
});             // end of onReady

