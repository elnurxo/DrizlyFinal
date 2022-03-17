$(document).ready(function () {
    //DELETE-FEATURE
    $(document).on("click", ".delete-service", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })

    //DELETE-FEATURE
    $(document).on("click", ".delete-partnership", function (e) {
        e.preventDefault();
        console.log("alkhgfaslg");
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //LOG-OUT USER
    $(document).on("click", ".logout-user", function (e) {
        e.preventDefault();
        console.log("salamlog");
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Log Out!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Logged Out!',
                            'Logged Out Successfully',
                            'success'
                        ).then(function () {
                            window.location = Response.url;
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Failed to Log Out!',
                            'success'
                        )
                    }
                })
            }
        })
    })
});
