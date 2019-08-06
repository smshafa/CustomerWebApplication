
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
        //exception: function (reader, response, error, eOpts) {
        //    var respObj = Ext.decode(response.responseText);
        //    if (respObj.success === false) {
        //        console.log(respObj.error);
        //        alert(respObj.error);
        //    }
        //},

        //exception: function (proxy, response, options) {
        //    requestMessageProcessor(proxy, response);
        //},

        //afterRequest: function (request, success) {
        //    requestMessageProcessor(request.scope, request.operation.response);
        //},


        write: function (store, operation) {
            var record = operation.getRecords()[0],
                name = Ext.String.capitalize(operation.action),
                verb;


            //alert('before');
            //alert(operation._response.responseText);
            //alert('after');

            var myResponse = JSON.parse(operation._response.responseText);
            //alert(myResponse.status);
            if (myResponse.status == 202) {
                Ext.example.msg(name, Ext.String.format(myResponse.message));
            }

            //if (operation._response.status == 200) {
            //    alert("ok");
            //}

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
                //grid.store.reload();
                store.reload();
            }
            if (name == 'Update') {
                alert('update');
                //grid.store.reload();
                store.reload();
                //Ext.example.msg(name, Ext.String.format("مشتری {1} بروز گردید.", verb, record.getId()));
            }

        }
    }
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
