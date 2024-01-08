// ==================================================
// * Project Name   :  Collab – Online Learning Platform Template
// * File           :  JS Base
// * Version        :  1.0.0
// * Last change    :  17 September 2022, Saturday
// * Author         :  Merkulove (https://themeforest.net/user/merkulove)
// * Developer      :  webrok (https://www.fiverr.com/webrok?up_rollout=true)
// ==================================================

(function($) {
  "use strict";

  // Back To Top - Start
  // --------------------------------------------------
  $(window).scroll(function() {
    if ($(this).scrollTop() > 200) {
      $('.backtotop:hidden').stop(true, true).fadeIn();
    } else {
      $('.backtotop').stop(true, true).fadeOut();
    }
  });
  $(function() {
    $(".scroll").on('click', function() {
      $("html,body").animate({scrollTop: 0}, "slow");
      return false
    });
  });
  // Back To Top - End
  // --------------------------------------------------

  // wow js - start
  // --------------------------------------------------
  var wow = new WOW({
    animateClass: 'animated',
    offset: 100,
    mobile: true,
    duration: 400,
  });
  wow.init();
  // wow js - end
  // --------------------------------------------------

  // Dropdown - Start
  // --------------------------------------------------
  $(document).ready(function () {
    $(".dropdown").on('mouseover', function () {
      $(this).find('> .dropdown-menu').addClass('show');
    });
    $(".dropdown").on('mouseout', function () {
      $(this).find('> .dropdown-menu').removeClass('show');
    });
  });
  // Dropdown - End
  // --------------------------------------------------

  // Common Carousels - Start
  // --------------------------------------------------
  $('.common_carousel_1col').slick({
    dots: true,
    speed: 1000,
    arrows: true,
    infinite: true,
    autoplay: true,
    slidesToShow: 1,
    pauseOnHover: true,
    autoplaySpeed: 5000,
    prevArrow: ".cc1c_left_arrow",
    nextArrow: ".cc1c_right_arrow"
  });

  $('.common_carousel_2col').slick({
    dots: true,
    speed: 1000,
    arrows: true,
    infinite: true,
    autoplay: true,
    slidesToShow: 2,
    slidesToScroll: 2,
    pauseOnHover: true,
    autoplaySpeed: 5000,
    prevArrow: ".cc2c_left_arrow",
    nextArrow: ".cc2c_right_arrow",
    responsive: [
    {
      breakpoint: 576,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 992,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    }
    ]
  });
  // Common Carousels - End
  // --------------------------------------------------

  // Chart Stock In - Start
  // --------------------------------------------------
  var configStockIn = document.getElementById('chart-stock-in').getContext('2d');
  var StockInChart = new Chart(configStockIn, {
    type: 'line',
    data: {
      labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
      datasets: [
      {
        label: "Stock In",
        fill: true,
        smooth: true,
        data: [0, 300, 400, 300, 500, 350, 800, 600, 500, 400, 1100],
        tension: 0.5,
        borderColor: "#00E0AA",
        backgroundColor: "rgba(0, 224, 170, 0.2)",
        pointStyle: 'circle',
        pointRadius: 5,
        pointHoverRadius: 10,
      }
      ]
    },
    options: {
      responsive: true,
      title: {
        display: true,
        text: "Stock In Chart"
      }
    }
  });
  // Chart Stock In - End
  // --------------------------------------------------

  // Chart Stock Out - Start
  // --------------------------------------------------
  var configStockOut = document.getElementById('chart-stock-out').getContext('2d');
  var StockOutChart = new Chart(configStockOut, {
    type: 'line',
    data: {
      labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
      datasets: [
      {
        label: "Stock Out",
        fill: true,
        smooth: true,
        data: [0, 300, 400, 300, 500, 350, 800, 600, 500, 400, 1100],
        tension: 0.5,
        borderColor: "#7652DC",
        backgroundColor: "rgba(118, 82, 220, 0.2)",
        pointStyle: 'circle',
        pointRadius: 5,
        pointHoverRadius: 10,
      }
      ]
    },
    options: {
      responsive: true,
      title: {
        display: true,
        text: "Stock Out Chart"
      }
    }
  });
  // Chart Stock Out - End
  // --------------------------------------------------

})(jQuery);