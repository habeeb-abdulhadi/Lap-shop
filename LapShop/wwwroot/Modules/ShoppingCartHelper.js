"use strict"

function onQtyChange(input) {

    //selections
    let itemId = $(input).data('itemid');
    let itemPrice = $(input).data('itemprice');
    let totalItemQty = $(input).val();
    let totalItemPrice = $(input).closest("tr").find(".totalItemPrice");


    if (totalItemQty <= 0) {
        removeFromCart(input);
        return;

    }
    else {

        //add to cart method call
        $.ajax({
            url: '/Order/AddToCart',
            data: { id: itemId, qty: totalItemQty },
            success: function (data) {

                let newTotalPrice = (parseFloat(parseFloat(itemPrice) * parseInt(totalItemQty)).toFixed(2))
                totalItemPrice.html(newTotalPrice);


                //changing Total price + in the DOM too
                UpdateShoppingCartQtyDOM(UpdateShoppingCartQty());
                UpdateShoppingCartPriceDOM(UpdateShoppingCartPrice());

                //update top shopping cart icon
                GetShoppingCart()

            },
            error: function (xhr, ajaxoptions, thrownerror) {
                alert(xhr.responsetext);
            }
        });
    }


}

function removeFromCart(obj) {

    let itemId = $(obj).data("itemid");

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
            GetShoppingCart()

        },
        error: function (xhr, ajaxoptions, thrownerror) {
            alert(xhr.responsetext);
        }
    });


}


$(document).ready(function () {

    console.log($(".add-To-Cart").length)

    $(".add-To-Cart").on('click', function () {



        let id = $(this).data("id");

        $.ajax({
            url: '/Order/AddToCart',
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
                GetShoppingCart();



            },
            error: function (xhr, ajaxoptions, thrownerror) {
                alert(xhr.responsetext);
            }
        });
    });



});



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
            GetShoppingCart()

        },
        error: function (xhr, ajaxoptions, thrownerror) {
            alert(xhr.responsetext);
        }
    });

}



function GetShoppingCart() {

    $.ajax({
        url: '/Order/GetShoppingCart',
        success: function (data) {

            if (data != null) {

                //update top cart icon number
                $(".TopShoppingCart").html(data.qty)


                //update top cart list
                $(".TopCartItemsList").html("");
                for (let item of data.lstItems) {

                    let itemBox = `<li> <div class="media">
                                                       
                                                        <div class="media-body">
                                                            <a href="#">
                                                                <h4> $${item.ItemName}</h4>
                                                            </a>
                                                           
                                                        </div>
                                                    </div>
                                                   
                                                </li>`;

                    $(".TopCartItemsList").append(itemBox);

                }


                // update top cart list totalQty and totalPrice
                $("#TopCartItemsQty").html(data.qty);
                $("#TopCartItemsPrice").html(data.Total);

            }

        },
        error: function (data) { }
    });


}







//private methods to use 

function UpdateShoppingCartPrice() {

    let totalShoppingCartPrice = 0;

    $(".cart-item-row").each(function (i) {

        totalShoppingCartPrice += parseFloat($(this).find(".totalItemPrice").html());
    })

    return totalShoppingCartPrice;
}

function UpdateShoppingCartQty() {

    let totalShoppingCartQty = 0;

    $(".cart-item-row").each(function (i) {
        totalShoppingCartQty += parseInt($(this).find(".totalItemQty").val());
    })
    return totalShoppingCartQty;

}


function UpdateShoppingCartPriceDOM(shoppingCartPrice) {
    $("#Total").html(shoppingCartPrice.toFixed(2));

}

function UpdateShoppingCartQtyDOM(shoppingCartQty) {
    $("#qty").html(shoppingCartQty);

}



