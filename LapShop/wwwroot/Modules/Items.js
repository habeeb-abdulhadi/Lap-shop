
var ClsItems = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7058/api/Items/ItemList", {}, "json",
            function (data) {



                $('#itemPagination').pagination({
                    dataSource: data.data,
                    pageSize: 16,
                    showGoInput: true,
                    showGoButton: true,
                    callback: function (data, pagination) {
                        // template method of yourself
                        var htmlData = "";
                        for (var i = 0; i < data.length; i++) {
                            htmlData += ClsItems.DrawItem(data[i]);
                            console.log(data[i].itemId);
                        }
                        var d1 = document.getElementById('ItemsArea');
                        d1.innerHTML = htmlData;
                    }
                });

            }, function () { });
    },

    DrawItem: function (item) {
        var data = "<div class='col-xl-3 col-6 col-grid-box'>";
        data += "<div class='product-box'><div class='img-wrapper'>";
        data += "<div class='front'> <a href='/Items/ItemDetails/" + item.itemId + "'><img src='/FrontEnd/images/imageLap/" + item.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
        data += "<div class='back'> <a href='/Items/ItemDetails/" + item.itemId + "'><img src='/FrontEnd/images/imageLap/" + item.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
        data += "<div class='cart-info cart-wrap'>";
        data += "<button class='add-To-Cart' onclick='Clscategorsss.sendidd(" + item.itemId + ")' title='Add to cart'><i class='ti-shopping-cart'></i></button>";
        data += "<a href='/Items/ItemDetails/" + item.itemId + "' title='Add to Wishlist'><i class='ti-heart' aria-hidden='true'></i></a>";
        data += "<a href='/Items/ItemDetails/" + item.itemId + "' data-toggle='modal' data-target='#quick - view' title='Quick View'><i class='ti-search' aria-hidden='true'></i></a>";
        data += "<a href='/Items/ItemDetails/" + item.itemId + "' title='Compare'><i class='ti-reload' aria-hidden='true'></i></a></div></div>";
        data += "<div class='product-detail'><div class='rating'> <i class='fa fa-star'></i> <i class='fa fa-star'></i> <i class='fa fa-star'></i>";
        data += "<i class='fa fa-star'></i> <i class='fa fa-star'></i></div>";
        data += "<a href='/Items/ItemDetails/" + item.itemId + "'><h6>" + item.itemName + "</h6></a> <p> </p>";
        data += "<h4>$" + item.salesPrice + "</h4>";
        data += "<ul class='color-variant'><li class='bg-light0'></li><li class='bg-light1'></li><li class='bg-light2'></li></ul> </div> </div> </div>";
        return data;

    }
}




var Clscategors = {
    GetAlll: function () {
        Helper.AjaxCallGet("https://localhost:7058/api/GetAllCategories", {}, "json",
            function (data) {

                // template method of yourself
                var htmlData = "";
                for (var i = 0; i < data.length; i++) {
                    htmlData += Clscategors.DrawItem(data[i]);
                
                }
                var d2 = document.getElementById('CatName');
                d2.innerHTML = htmlData;
                var d3 = document.getElementById('all');
                d3.innerHTML += " <div class='custom-control custom-checkbox collection-filter-checkbox'>";
                d3.innerHTML += " <button type='button' class='btn btnCatFilter btn-all' onclick='ClsItems.GetAll()' >All</button > </div > ";
            }, function () { });
    },

    DrawItem: function (_item) { 
        var data = " <div class='custom-control custom-checkbox collection-filter-checkbox'>" + _item.categoryId + "";
        data += " <button type='button' class='btn btnCatFilter btn-all' onclick='Clscategorss.sendid(" + _item.categoryId + ")' >" + _item.categoryName + "</button > </div > ";       
        return data;
      
    }
}



 
var Clscategorss = {
 sendid:function (userId) {
    //alert(userId);
        Helper.AjaxCallGet("https://localhost:7058/api/Items/GetByCategoryId/" + userId, {}, "json",
        function (data) {



            $('#itemPagination').pagination({
                dataSource: data.data,
                pageSize: 16,
                showGoInput: true,
                showGoButton: true,
                callback: function (data, pagination) {
                    // template method of yourself
                    var htmlData = "";
                    for (var i = 0; i < data.length; i++) {
                        htmlData += Clscategorss.DrawItemm(data[i]);
                    //    console.log(data[i])
                    }
                    var d1 = document.getElementById('ItemsArea');
                    d1.innerHTML = htmlData;
                }
            });
           
        });
},
 DrawItemm: function(_1item) {
            var data = "<div class='col-xl-3 col-6 col-grid-box'>";
     data += "<div class='product-box'><div class='img-wrapper'>";
     data += "<div class='front'> <a href='/Items/ItemDetails/" + _1item.itemId + "'><img src='/FrontEnd/images/imageLap/" + _1item.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
     data += "<div class='back'> <a href='/Items/ItemDetails/" + _1item.itemId + "'><img src='/FrontEnd/images/imageLap/" + _1item.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
     data += "<div class='cart-info cart-wrap'>";
     data += "<button class='add-To-Cart' onclick='Clscategorsss.sendidd(" + _1item.itemId + ")' title='Add to cart'><i class='ti-shopping-cart'></i></button>";
     data += "<a href='/Items/ItemDetails/" + _1item.itemId + "' title='Add to Wishlist'><i class='ti-heart' aria-hidden='true'></i></a>";
     data += "<a href='/Items/ItemDetails/" + _1item.itemId + "' data-toggle='modal' data-target='#quick - view' title='Quick View'><i class='ti-search' aria-hidden='true'></i></a>";
     data += "<a href='/Items/ItemDetails/" + _1item.itemId + "' title='Compare'><i class='ti-reload' aria-hidden='true'></i></a></div></div>";
            data += "<div class='product-detail'><div class='rating'> <i class='fa fa-star'></i> <i class='fa fa-star'></i> <i class='fa fa-star'></i>";
            data += "<i class='fa fa-star'></i> <i class='fa fa-star'></i></div>";
     data += "<a href='/Items/ItemDetails/" + _1item.itemId + "'><h6>" + _1item.itemName + "</h6></a> <p> </p>";
     data += "<h4>$" + _1item.salesPrice + "</h4>";
            data += "<ul class='color-variant'><li class='bg-light0'></li><li class='bg-light1'></li><li class='bg-light2'></li></ul> </div> </div> </div>";
            return data;
}
}


var ClsCart = {
    GetAllll: function () {
        Helper.AjaxCallGet("https://localhost:7058/Order/GetShoppingCart", {}, "json",
            function (data) {

                // template method of yourself
                var htmlData = "";
                console.log(data);
                for (var i = 0; i < data.lstItems.length; i++) {
                    htmlData += ClsCart.DrawItem(data.lstItems[i]);

                    //    console.log(data[i].categoryId);
                }
                var d2 = document.getElementById('TopCartItemsList');
                d2.innerHTML = htmlData;
                var d3 = document.getElementById('TopCartItemsQty');
                d3.innerHTML = data.qty;
                var d4 = document.getElementById('TopCartItemsPrice');
                d4.innerHTML=data.total

            }, function () { });
    },

    DrawItem: function (_2item) {
        var data = "<li> <div class='media'>";
        data += "<a href='#'> <img alt='' class='mr-3'src='/FrontEnd/Images/imageLap/" + _2item.imageName + "'";
        data += "</a><div class='media-body' ><a href='#'><h4> " + _2item.itemName + "</h4></a>";
        data += "<h6>Price: <span> " + _2item.price + "</span><br/>Qty: <span>" + _2item.qty + "x</span> <br/>Total Price: <span> " + _2item.total + "</span> </h6></div></div> ";
        data += " <div class='close-circle'> <a data-id='" + _2item.itemId + "' onclick='RemoveFromTopCart(this)' ><i class='fa fa-times' aria-hidden='true'></i></a></div> </li> ";
                                                        
                                                   
        return data;

    }
}

var Clscategorsss = {
    sendidd: function (id) {


        console.log(id);

        $.ajax({
            url: '/Order/addtocart',
            data: { id: id },
            success: function (data) {


                $.notify({
                    icon: 'fa fa-check',
                    title: 'Success!',
                    message: 'Item Successfully added to your cart'
                }, {
                    element: 'body',
                    position: null,
                    type: "success",
                    allow_dismiss: true,
                    newest_on_top: false,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    offset: 20,
                    spacing: 10,
                    z_index: 1031,
                    delay: 300,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });

                //get shopping cart + get top shopping cart icon + top cart list 
                ClsCart.GetAllll();



            },
            error: function (xhr, ajaxoptions, thrownerror) {
                alert(xhr.responsetext);
            }
        });
  
}

}

function onQtyChange(input) {

    //selections
    let itemId = $(input).data('id');
    let itemPrice = $(input).data('price');
    let totalItemQty = $(input).val();
    let totalItemPrice = $(input).closest("tr").find(".totalPrice");


    if (totalItemQty <= 0) {
        removeFromCart(input);
        return;

    }
    else {

        //add to cart method call
        $.ajax({
            url: '/Order/addtocart',
            data: { id: itemId, qty: totalItemQty },
            success: function (data) {

                let newTotalPrice = (parseFloat(parseFloat(itemPrice) * parseInt(totalItemQty)).toFixed(2))
                console.log(newTotalPrice);
                totalItemPrice.html(newTotalPrice);


                //changing Total price + in the DOM too
                UpdateShoppingCartQtyDOM(UpdateShoppingCartQty());
                UpdateShoppingCartPriceDOM(UpdateShoppingCartPrice());

                //update top shopping cart icon
                ClsCart.GetAllll();
            },
            error: function (xhr, ajaxoptions, thrownerror) {
                alert(xhr.responsetext);
            }
        });
    }


}

function removeFromCart(obj) {

    let itemId = $(obj).data("id");

    //remove from cart method call
    $.ajax({
        url: '/Order/RemoveFromCart',
        data: { id: itemId },
        success: function (data) {

            obj.closest("tr").remove();

            //changing Total price + in the DOM too
            UpdateShoppingCartQtyDOM(UpdateShoppingCartQty());
            UpdateShoppingCartPriceDOM(UpdateShoppingCartPrice());

            //update top shopping cart icon + top cart list 
            ClsCart.GetAllll();
        },
        error: function (xhr, ajaxoptions, thrownerror) {
            alert(xhr.responsetext);
        }
    });


}
function RemoveFromTopCart(obj) {

    var itemId = $(obj).data("id");

    $.ajax({
        url: '/Order/RemoveFromCart',
        data: { id: itemId },
        success: function (data) {

            //obj.closest("tr").remove();

            //changing Total price + in the DOM too
            UpdateShoppingCartQtyDOM(UpdateShoppingCartQty());
            UpdateShoppingCartPriceDOM(UpdateShoppingCartPrice());

            ////update top shopping cart icon + top cart list 
            ClsCart.GetAllll();
        },
        error: function (xhr, ajaxoptions, thrownerror) {
            alert(xhr.responsetext);
        }
    });

}


function UpdateShoppingCartPrice() {

    let totalShoppingCartPrice = 0;

    $(".cart-item-row").each(function (i) {

        totalShoppingCartPrice += parseFloat($(this).find(".totalPrice").html());
        console.log(totalShoppingCartPrice);
    })

    return totalShoppingCartPrice;
}

function UpdateShoppingCartQty() {

    let totalShoppingCartQty = 0;

    $(".cart-item-row").each(function (i) {
        totalShoppingCartQty += parseInt($(this).find(".qty").val());
        console.log(totalShoppingCartQty);
    })
    return totalShoppingCartQty;

}


function UpdateShoppingCartPriceDOM(shoppingCartPrice) {
    $("#totalShoppingCartPrice").html(shoppingCartPrice.toFixed(2));

}

function UpdateShoppingCartQtyDOM(shoppingCartQty) {
    $("#totalShoppingCartQty").html(shoppingCartQty);

}
