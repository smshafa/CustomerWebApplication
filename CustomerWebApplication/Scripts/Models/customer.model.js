
Ext.define('Customer', {
    extend: 'Ext.data.Model',
    idProperty: 'customerId',
    fields: [{
        name: 'CustomerID',
        type: 'int',
        //useNull: true
    }, 'FirstName', 'LastName', 'CityName', 'ProvinceName'],
    validations: [
        //    {
        //    type: 'length',
        //    field: 'FirstName',
        //    min: 1,
        //    max: 60
        //},
        {
            type: 'length',
            field: 'LastName',
            min: 1,
            max: 60
        }
        //    , {
        //    type: 'notnull',
        //    field: 'FirstName',
        //    message: 'not null'
        //}
    ]
});

Ext.define('ProvinceModel', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'ProvinceID',
        type: 'int'
    }, {
        name: 'ProvinceName',
        type: 'string'
    }
    ]
});

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
