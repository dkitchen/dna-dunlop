--------------------------------------------------------
--  DDL for View EVENT_LOG_VIEW_WITH_SHIFT
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORACLE"."EVENT_LOG_VIEW_WITH_SHIFT" ("EVENT_LOG_ID", "EVENT_CREATED", "EVENT_ID", "EVENT_NAME", "MACHINE_ID", "MACHINE_NAME", "PART_ID", "PART_CREATED", "PART_LABEL", "GREEN_TIRE_NUMBER", "GOODYEAR_SERIAL_NUMBER", "PART_HISTORY_LABEL", "PART_HISTORY_GTN", "PART_HISTORY_GSN", "OPERATOR_ID", "EMPLOYEE_NUMBER", "OPERATOR", "EVENT_DATA_ID", "DATA_NAME", "DATA_VALUE", "WORK_DATE", "WORK_SHIFT") AS 
  select 
l.event_log_id, 
l.created event_created,
e.event_id,
e.name event_name,
m.machine_id,
m.name machine_name,
p.part_id,
p.created part_created,
p.serial_number part_label,
p.green_tire_number,
p.goodyear_serial_number,
ph.serial_number part_history_label,
ph.green_tire_number part_history_gtn,
ph.goodyear_serial_number part_history_gsn,
o.operator_id,
o.employee_number,
case
when o.name = 'UNKNOWN' then o.name || ' (' || o.serial_number || ')'
else o.name || ' (' || o.employee_number || ')' 
END operator,
ed.event_data_id,
ed.name data_name,
d.data_value,
ls.work_date,
ls.work_shift
from event_log l
join event_log_shift ls ON l.event_log_id = ls.event_log_id
left join event e on l.event_id = e.event_id
left join machine m on l.machine_id = m.machine_id
left join part p on l.part_id = p.part_id
left join part_history ph on l.part_id = ph.part_id
left join event_log_data d on l.event_log_id = d.event_log_id
left join event_data ed on d.event_data_id = ed.event_data_id
left join operator o on l.operator_id = o.operator_id;
