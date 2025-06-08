using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Models.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TicketBus.Areas.Brand.Controllers
{
    [Area("Brand")]
    [Authorize(Roles = "Brand")]
    public class BusRouteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BusRouteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Hiển thị form đăng ký tuyến (không điền sẵn dữ liệu)
        public IActionResult Create()
        {
            var model = new BusRouteViewModel
            {
                RouteStops = new List<RouteStopViewModel>
                {
                    new RouteStopViewModel()
                },
                Pickups = new List<PickupViewModel>(),
                DropOffs = new List<DropOffViewModel>(),
                Brands = _context.Brands
                    .Select(b => new SelectListItem
                    {
                        Value = b.IdBrand.ToString(),
                        Text = b.NameBrand
                    })
                    .ToList(),
                Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList()
            };

            foreach (var stop in model.RouteStops)
            {
                stop.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            foreach (var pickup in model.Pickups)
            {
                pickup.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            foreach (var dropoff in model.DropOffs)
            {
                dropoff.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            return View(model);
        }

        // POST: Thêm điểm dừng
        [HttpPost]
        public IActionResult AddRouteStop(BusRouteViewModel model)
        {
            if (model.RouteStops == null)
            {
                model.RouteStops = new List<RouteStopViewModel>();
            }

            foreach (var stop in model.RouteStops)
            {
                stop.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            var newStop = new RouteStopViewModel
            {
                StopOrder = model.RouteStops.Count,
                Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList()
            };
            newStop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            model.RouteStops.Add(newStop);

            model.Brands = _context.Brands
                .Select(b => new SelectListItem
                {
                    Value = b.IdBrand.ToString(),
                    Text = b.NameBrand
                })
                .ToList();
            model.Cities = _context.Cities
                .Select(c => new SelectListItem
                {
                    Value = c.IdCity.ToString(),
                    Text = c.NameCity
                })
                .ToList();

            if (model.Pickups == null)
            {
                model.Pickups = new List<PickupViewModel>();
            }
            foreach (var pickup in model.Pickups)
            {
                pickup.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            if (model.DropOffs == null)
            {
                model.DropOffs = new List<DropOffViewModel>();
            }
            foreach (var dropoff in model.DropOffs)
            {
                dropoff.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            return View("Create", model);
        }

        // POST: Thêm điểm đón
        [HttpPost]
        public IActionResult AddPickup(BusRouteViewModel model)
        {
            if (model.Pickups == null)
            {
                model.Pickups = new List<PickupViewModel>();
            }

            var newPickup = new PickupViewModel
            {
                Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList()
            };
            newPickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            model.Pickups.Add(newPickup);

            model.Brands = _context.Brands
                .Select(b => new SelectListItem
                {
                    Value = b.IdBrand.ToString(),
                    Text = b.NameBrand
                })
                .ToList();
            model.Cities = _context.Cities
                .Select(c => new SelectListItem
                {
                    Value = c.IdCity.ToString(),
                    Text = c.NameCity
                })
                .ToList();

            foreach (var stop in model.RouteStops)
            {
                stop.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            foreach (var pickup in model.Pickups)
            {
                pickup.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            if (model.DropOffs == null)
            {
                model.DropOffs = new List<DropOffViewModel>();
            }
            foreach (var dropoff in model.DropOffs)
            {
                dropoff.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            return View("Create", model);
        }

        // POST: Thêm điểm trả
        [HttpPost]
        public IActionResult AddDropOff(BusRouteViewModel model)
        {
            if (model.DropOffs == null)
            {
                model.DropOffs = new List<DropOffViewModel>();
            }

            var newDropOff = new DropOffViewModel
            {
                Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList()
            };
            newDropOff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            model.DropOffs.Add(newDropOff);

            model.Brands = _context.Brands
                .Select(b => new SelectListItem
                {
                    Value = b.IdBrand.ToString(),
                    Text = b.NameBrand
                })
                .ToList();
            model.Cities = _context.Cities
                .Select(c => new SelectListItem
                {
                    Value = c.IdCity.ToString(),
                    Text = c.NameCity
                })
                .ToList();

            foreach (var stop in model.RouteStops)
            {
                stop.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            if (model.Pickups == null)
            {
                model.Pickups = new List<PickupViewModel>();
            }
            foreach (var pickup in model.Pickups)
            {
                pickup.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            foreach (var dropoff in model.DropOffs)
            {
                dropoff.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            return View("Create", model);
        }

        // POST: Xử lý đăng ký tuyến
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BusRouteViewModel model)
        {
            if (model.RouteStops == null)
            {
                model.RouteStops = new List<RouteStopViewModel>();
            }
            if (model.Pickups == null)
            {
                model.Pickups = new List<PickupViewModel>();
            }
            if (model.DropOffs == null)
            {
                model.DropOffs = new List<DropOffViewModel>();
            }

            if (ModelState.IsValid)
            {
                var stopNames = model.RouteStops.Select(rs => rs.StopName).ToList();
                if (stopNames.Any(sn => !string.IsNullOrEmpty(sn)) && stopNames.Where(sn => !string.IsNullOrEmpty(sn)).Distinct().Count() != stopNames.Count(sn => !string.IsNullOrEmpty(sn)))
                {
                    ModelState.AddModelError("", "Các điểm dừng (nếu có tên) không được trùng tên.");
                }

                if (model.RouteStops.Count < 1)
                {
                    ModelState.AddModelError("", "Tuyến xe phải có ít nhất 1 điểm dừng.");
                }

                for (int i = 1; i < model.RouteStops.Count; i++)
                {
                    if (string.IsNullOrEmpty(model.RouteStops[i].Time) || string.IsNullOrEmpty(model.RouteStops[i - 1].Time))
                        continue;

                    var prevTime = TimeSpan.Parse(model.RouteStops[i - 1].Time);
                    var currTime = TimeSpan.Parse(model.RouteStops[i].Time);
                    if (currTime <= prevTime)
                    {
                        ModelState.AddModelError("", "Thời gian của điểm dừng sau phải lớn hơn điểm dừng trước.");
                        break;
                    }
                }

                if (!model.IdStartCity.HasValue || !await _context.Cities.AnyAsync(c => c.IdCity == model.IdStartCity.Value))
                {
                    ModelState.AddModelError("IdStartCity", "Thành phố xuất phát không tồn tại.");
                }
                if (!model.IdEndCity.HasValue || !await _context.Cities.AnyAsync(c => c.IdCity == model.IdEndCity.Value))
                {
                    ModelState.AddModelError("IdEndCity", "Thành phố kết thúc không tồn tại.");
                }

                foreach (var stop in model.RouteStops)
                {
                    if (stop.IdCity.HasValue && !await _context.Cities.AnyAsync(c => c.IdCity == stop.IdCity.Value))
                    {
                        ModelState.AddModelError("", $"Thành phố không hợp lệ cho điểm dừng {stop.StopName ?? "không tên"}.");
                        break;
                    }
                }

                if (model.Pickups != null && model.Pickups.Any())
                {
                    var pickupNames = model.Pickups.Select(p => p.PickupName).ToList();
                    if (pickupNames.Any(pn => string.IsNullOrEmpty(pn)))
                    {
                        ModelState.AddModelError("Pickups", "Tên điểm đón không được để trống.");
                    }
                    else if (pickupNames.Distinct().Count() != pickupNames.Count)
                    {
                        ModelState.AddModelError("Pickups", "Các điểm đón không được trùng tên.");
                    }

                    foreach (var pickup in model.Pickups)
                    {
                        if (!pickup.IdCity.HasValue || !await _context.Cities.AnyAsync(c => c.IdCity == pickup.IdCity.Value))
                        {
                            ModelState.AddModelError("Pickups", "Thành phố của điểm đón không hợp lệ.");
                            break;
                        }
                    }
                }

                if (model.DropOffs != null && model.DropOffs.Any())
                {
                    var dropOffNames = model.DropOffs.Select(d => d.DropOffName).ToList();
                    if (dropOffNames.Any(dn => string.IsNullOrEmpty(dn)))
                    {
                        ModelState.AddModelError("DropOffs", "Tên điểm trả không được để trống.");
                    }
                    else if (dropOffNames.Distinct().Count() != dropOffNames.Count)
                    {
                        ModelState.AddModelError("DropOffs", "Các điểm trả không được trùng tên.");
                    }

                    foreach (var dropoff in model.DropOffs)
                    {
                        if (!dropoff.IdCity.HasValue || !await _context.Cities.AnyAsync(c => c.IdCity == dropoff.IdCity.Value))
                        {
                            ModelState.AddModelError("DropOffs", "Thành phố của điểm trả không hợp lệ.");
                            break;
                        }
                    }
                }

                if (!ModelState.IsValid)
                {
                    model.Brands = _context.Brands
                        .Select(b => new SelectListItem
                        {
                            Value = b.IdBrand.ToString(),
                            Text = b.NameBrand
                        })
                        .ToList();
                    model.Cities = _context.Cities
                        .Select(c => new SelectListItem
                        {
                            Value = c.IdCity.ToString(),
                            Text = c.NameCity
                        })
                        .ToList();

                    foreach (var stop in model.RouteStops)
                    {
                        stop.Cities = _context.Cities
                            .Select(c => new SelectListItem
                            {
                                Value = c.IdCity.ToString(),
                                Text = c.NameCity
                            })
                            .ToList();
                        stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
                    }

                    foreach (var pickup in model.Pickups)
                    {
                        pickup.Cities = _context.Cities
                            .Select(c => new SelectListItem
                            {
                                Value = c.IdCity.ToString(),
                                Text = c.NameCity
                            })
                            .ToList();
                        pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
                    }

                    foreach (var dropoff in model.DropOffs)
                    {
                        dropoff.Cities = _context.Cities
                            .Select(c => new SelectListItem
                            {
                                Value = c.IdCity.ToString(),
                                Text = c.NameCity
                            })
                            .ToList();
                        dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
                    }

                    return View(model);
                }

                var brand = await _context.Brands
                    .Include(b => b.RegistForm)
                    .FirstOrDefaultAsync(b => b.IdBrand == model.IdBrand);
                if (brand == null)
                {
                    ModelState.AddModelError("IdBrand", "Hãng xe không tồn tại.");
                    model.Brands = _context.Brands
                        .Select(b => new SelectListItem
                        {
                            Value = b.IdBrand.ToString(),
                            Text = b.NameBrand
                        })
                        .ToList();
                    model.Cities = _context.Cities
                        .Select(c => new SelectListItem
                        {
                            Value = c.IdCity.ToString(),
                            Text = c.NameCity
                        })
                        .ToList();

                    foreach (var stop in model.RouteStops)
                    {
                        stop.Cities = _context.Cities
                            .Select(c => new SelectListItem
                            {
                                Value = c.IdCity.ToString(),
                                Text = c.NameCity
                            })
                            .ToList();
                        stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
                    }

                    foreach (var pickup in model.Pickups)
                    {
                        pickup.Cities = _context.Cities
                            .Select(c => new SelectListItem
                            {
                                Value = c.IdCity.ToString(),
                                Text = c.NameCity
                            })
                            .ToList();
                        pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
                    }

                    foreach (var dropoff in model.DropOffs)
                    {
                        dropoff.Cities = _context.Cities
                            .Select(c => new SelectListItem
                            {
                                Value = c.IdCity.ToString(),
                                Text = c.NameCity
                            })
                            .ToList();
                        dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
                    }

                    return View(model);
                }
                int? idRegist = brand.RegistFormId;

                int routeCount = await _context.BusRoutes.CountAsync() + 1;
                string routeCode = $"BR-{routeCount:D3}";

                int stopCount = await _context.RouteStops.CountAsync();
                var routeStops = model.RouteStops.Select((rs, index) =>
                {
                    stopCount++;
                    TimeSpan? timeSpan = null;
                    if (!string.IsNullOrEmpty(rs.Time))
                    {
                        var parts = rs.Time.Split(':');
                        timeSpan = new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), 0);
                    }

                    return new RouteStop
                    {
                        StopCode = $"STOP-{stopCount:D3}",
                        StopName = rs.StopName,
                        IdCity = rs.IdCity.Value, // Ép kiểu từ int? sang int
                        StopOrder = index,
                        Time = timeSpan
                    };
                }).ToList();

                var pickups = model.Pickups.Select(p => new Pickup
                {
                    PickupName = p.PickupName,
                    IdCity = p.IdCity.Value, // Ép kiểu từ int? sang int
                    IdBrand = model.IdBrand
                }).ToList();

                var dropoffs = model.DropOffs.Select(d => new DropOff
                {
                    DropOffName = d.DropOffName,
                    IdCity = d.IdCity.Value, // Ép kiểu từ int? sang int
                    IdBrand = model.IdBrand
                }).ToList();

                TimeSpan? travelTime = null;
                if (!string.IsNullOrEmpty(model.TravelTime))
                {
                    var parts = model.TravelTime.Split(':');
                    travelTime = new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), 0);
                }

                var busRoute = new BusRoute
                {
                    RouteCode = routeCode,
                    NameRoute = model.NameRoute,
                    Distance = model.Distance,
                    IdBrand = model.IdBrand,
                    IdRegist = idRegist,
                    IdStartCity = model.IdStartCity,
                    IdEndCity = model.IdEndCity,
                    State = BusRouteState.ChoPheDuyet,
                    TravelTime = travelTime,
                    StartDate = model.StartDate,
                    RouteStops = routeStops,
                    Pickups = pickups,
                    DropOffs = dropoffs
                };

                _context.Add(busRoute);
                await _context.SaveChangesAsync();

                // Tạo thông báo cho người dùng
                var userId = _userManager.GetUserId(User);
                var notification = new Notification
                {
                    UserId = userId,
                    Message = $"Đăng ký tuyến xe '{model.NameRoute}' thành công và đang chờ phê duyệt.",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đăng ký tuyến xe thành công! Vui lòng chờ admin phê duyệt.";
                return RedirectToAction(nameof(Index));
            }

            model.Brands = _context.Brands
                .Select(b => new SelectListItem
                {
                    Value = b.IdBrand.ToString(),
                    Text = b.NameBrand
                })
                .ToList();
            model.Cities = _context.Cities
                .Select(c => new SelectListItem
                {
                    Value = c.IdCity.ToString(),
                    Text = c.NameCity
                })
                .ToList();

            foreach (var stop in model.RouteStops)
            {
                stop.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                stop.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            foreach (var pickup in model.Pickups)
            {
                pickup.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                pickup.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            foreach (var dropoff in model.DropOffs)
            {
                dropoff.Cities = _context.Cities
                    .Select(c => new SelectListItem
                    {
                        Value = c.IdCity.ToString(),
                        Text = c.NameCity
                    })
                    .ToList();
                dropoff.Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn thành phố" });
            }

            return View(model);
        }

        // GET: Hiển thị danh sách tuyến xe của Brand hiện tại, đã được phê duyệt
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Không thể xác định người dùng. Vui lòng đăng nhập lại.";
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var brand = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (brand == null)
            {
                TempData["Message"] = "Hãng xe của bạn chưa được phê duyệt hoặc không tồn tại.";
                return View(new List<BusRoute>());
            }

            var busRoutes = await _context.BusRoutes
                .Where(r => r.IdBrand == brand.IdBrand && r.State == BusRouteState.DaPheDuyet)
                .Include(r => r.Brand)
                .Include(r => r.StartCity)
                .Include(r => r.EndCity)
                .Include(r => r.RouteStops)
                .Include(r => r.Pickups)
                .Include(r => r.DropOffs)
                .ToListAsync();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }

            return View(busRoutes);
        }

        // GET: /Brand/BusRoute/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Không thể xác định người dùng. Vui lòng đăng nhập lại.";
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var brand = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId);
            if (brand == null)
            {
                TempData["Message"] = "Hãng xe của bạn chưa được phê duyệt hoặc không tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            var busRoute = await _context.BusRoutes
                .Include(r => r.StartCity)
                .Include(r => r.EndCity)
                .Include(r => r.Brand)
                .Include(r => r.RouteStops)
                .Include(r => r.Pickups)
                .Include(r => r.DropOffs)
                .FirstOrDefaultAsync(r => r.IdRoute == id && r.IdBrand == brand.IdBrand);

            if (busRoute == null)
            {
                return NotFound();
            }

            return View(busRoute);
        }
    }
}