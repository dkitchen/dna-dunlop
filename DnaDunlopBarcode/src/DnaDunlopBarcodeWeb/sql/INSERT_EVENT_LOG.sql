--------------------------------------------------------
--  DDL for Procedure INSERT_EVENT_LOG
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "ORACLE"."INSERT_EVENT_LOG" 
(
event_name IN NVARCHAR2,
machine_name IN NVARCHAR2 default NULL,
part_serial_number IN VARCHAR2 default NULL,
operator_serial_number IN VARCHAR2 default NULL,
data_name_1 IN VARCHAR2 default NULL,
data_value_1 IN NVARCHAR2 default NULL,
data_name_2 IN VARCHAR2 default NULL,
data_value_2 IN NVARCHAR2 default NULL,
data_name_3 IN VARCHAR2 default NULL,
data_value_3 IN NVARCHAR2 default NULL,
data_name_4 IN VARCHAR2 default NULL,
data_value_4 IN NVARCHAR2 default NULL,
data_name_5 IN VARCHAR2 default NULL,
data_value_5 IN NVARCHAR2 default NULL,
data_name_6 IN VARCHAR2 default NULL,
data_value_6 IN NVARCHAR2 default NULL,
data_name_7 IN VARCHAR2 default NULL,
data_value_7 IN NVARCHAR2 default NULL,
data_name_8 IN VARCHAR2 default NULL,
data_value_8 IN NVARCHAR2 default NULL,
data_name_9 IN VARCHAR2 default NULL,
data_value_9 IN NVARCHAR2 default NULL,
data_name_10 IN VARCHAR2 default NULL,
data_value_10 IN NVARCHAR2 default NULL,
data_name_11 IN VARCHAR2 default NULL,
data_value_11 IN NVARCHAR2 default NULL,
data_name_12 IN VARCHAR2 default NULL,
data_value_12 IN NVARCHAR2 default NULL,
data_name_13 IN VARCHAR2 default NULL,
data_value_13 IN NVARCHAR2 default NULL,
data_name_14 IN VARCHAR2 default NULL,
data_value_14 IN NVARCHAR2 default NULL,
data_name_15 IN VARCHAR2 default NULL,
data_value_15 IN NVARCHAR2 default NULL,
data_name_16 IN VARCHAR2 default NULL,
data_value_16 IN NVARCHAR2 default NULL,
data_name_17 IN VARCHAR2 default NULL,
data_value_17 IN NVARCHAR2 default NULL,
data_name_18 IN VARCHAR2 default NULL,
data_value_18 IN NVARCHAR2 default NULL,
data_name_19 IN VARCHAR2 default NULL,
data_value_19 IN NVARCHAR2 default NULL,
data_name_20 IN VARCHAR2 default NULL,
data_value_20 IN NVARCHAR2 default NULL)

is

  err_code Number;
  err_msg varchar2(500);
  
  new_event_log_id EVENT_LOG.EVENT_LOG_ID%TYPE;
  part_serial_15 varchar2(15);
  
  --vars for gti_cured
  prev_cure_active_time DATE;
  prev_tire PART.SERIAL_NUMBER%TYPE;
  cure_active_time DATE;
  cure_complete_time DATE;
  cycle_seconds NUMBER;
  cure_seconds NUMBER;
  elapsed_seconds NUMBER;
  
  --handy general purpose number var (used in POT setup for now)
  v_number NUMBER;
  
begin


  --get id of new event log to relate al this info to
  select EVENT_LOG_SEQUENCE.NEXTVAL INTO new_event_log_id from dual;
  
  --fix bug where part_serial_number sometimes comes in with 17 characaters???
  -- only take first 15 characters
  part_serial_15 := substr( part_serial_number, 1, 15);
  
  --fix bug where part serials can have carriage returns or line feeds
  part_serial_15 := replace(replace(part_serial_15, chr(10), ''), chr(13), '');
  
  
  
  
  INSERT INTO EVENT_LOG(
    EVENT_LOG_ID,
    EVENT_ID,
    MACHINE_ID,
    PART_ID,
    OPERATOR_ID )
  VALUES (
    new_event_log_id,
    GET_EVENT_ID( event_name ),
    GET_MACHINE_ID( machine_name ),
    GET_PART_ID( part_serial_15 ),
    GET_OPERATOR_ID( operator_serial_number ) );
    
  INSERT INTO EVENT_LOG_SHIFT (EVENT_LOG_ID, WORK_DATE, WORK_SHIFT)
  VALUES (new_event_log_id, GET_WORK_DATE(sysdate), GET_WORK_SHIFT(sysdate));
   
   
    --SAVE DATA VALUES RELATED TO THIS EVENT
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_1, data_value_1 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_2, data_value_2 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_3, data_value_3 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_4, data_value_4 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_5, data_value_5 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_6, data_value_6 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_7, data_value_7 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_8, data_value_8 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_9, data_value_9 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_10, data_value_10 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_11, data_value_11 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_12, data_value_12 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_13, data_value_13 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_14, data_value_14 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_15, data_value_15 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_16, data_value_16 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_17, data_value_17 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_18, data_value_18 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_19, data_value_19 );
    INSERT_EVENT_LOG_DATA( new_event_log_id, data_name_20, data_value_20 );

    
    --handle certain events
    --if this is the event for tire complete log to gti that this is in-stock. don't allow dups!
    IF event_name = 'GoodTire' THEN
      insert into gti_export_built (
        machine_number,
        green_tire_number,
        label,
        operator_winpak_id,
        label_scanned )
      SELECT 
        machine_name,
        p.GREEN_TIRE_NUMBER,
        p.SERIAL_NUMBER,
        operator_serial_number,
        p.CREATED
      FROM PART p
        --outer join to gti table to prevent dups
        left outer join gti_export_built eb on p.serial_number = eb.label
        where p.serial_number = part_serial_15
        and eb.label IS NULL;
    END IF;
    
    --log for gti curing 
    IF event_name = 'CureActive' THEN
     
      INSERT_GTI_EXPORT_CURED(part_serial_15, operator_serial_number, machine_name);

    END IF;

    --if Green tire mismatch, send a message to the operator to scan again
    --DMK 2010-03-04 disabled. Omni has return codes for doing this type of thing
    IF event_name = 'GreenTireMismatch' THEN
      --this assumes that first data param is the client address to send the message to
      insert into message ( address, message_code, message )
      values ( data_value_1, 'GreenTireMismatch', 'Green Tire Mismatch on ' || machine_name || '. Rescan or replace tire.');
    END IF;

    IF event_name = 'PotSetup' THEN
     --P_DEPARTMENT IN NUMBER  
      --, P_POT IN VARCHAR2  
      --, P_GREEN_TIRE_NUMBER IN VARCHAR2  
      --, P_TOTAL_CYCLE_TIME IN VARCHAR2  
      --, P_MOLD IN VARCHAR2  
      --, P_CURE_NUMBER IN VARCHAR2  
      --, P_SKU IN VARCHAR2  
      UPDATE_CURING_SETUP(data_value_1, data_value_2, data_value_3, data_value_4, data_value_5, data_value_6, data_value_7);
    END IF;

  --DMK 2011-11-25 - save GOODTIRE in its own log to optimize avg_cycle_time lookups
    IF event_Name = 'GoodTire' THEN
      INSERT_GOODTIRE_LOG(new_event_log_id);
    END IF;
    
    --DMK 2011-11-07 - handle event_card related events
    IF event_name = 'GoodTire'
      OR event_name = 'OperatorChange'
      OR event_name = 'Downtime'
      OR event_name = 'MachineSetup' THEN
      INSERT_EVENT_CARD_LOG(
        event_name, operator_serial_number, machine_name, part_serial_15, 
        data_value_1, 
        data_value_2, 
        data_value_3, data_value_4, data_value_5
        , data_name_1);
    END IF;
    
    --DMK 2011-12-17 - handle notification events
    IF event_name = 'Page' THEN
      INSERT_NOTIFICATION_LOG(        
        machine_name
        , operator_serial_number        
        , data_value_1    --call_type
        , data_value_2    --priority
        , data_value_3    --work_center
        , data_value_4    --symptom
        , data_value_5    --comment
        );

    END IF;


    IF event_name = 'CureTireLog' THEN
            
      HANDLE_CURE_TIRE_LOG(new_event_log_id, part_serial_15, data_value_2);
                  
    END IF;
    
    
    --DMK handle ﻿PartGreenTireNumberChange
    --  part_serial_15 SHOULD ONLY BE 10 
    --    OR YOU RISK Duplicating goodyear serial numbers in Part table
    -- data_value_1 is the NEW GTN
    IF event_name = 'PartGreenTireNumberChange' THEN
      HANDLE_GTN_CHANGE(new_event_log_id, part_serial_15, data_value_1);
    END IF;
    
    -- data_value_1 is the NEW Serial Number
    IF event_name = 'PartSerialNumberChange' THEN
      HANDLE_PSN_CHANGE(new_event_log_id, part_serial_15, data_value_1);
    END IF;


    EXCEPTION
      WHEN OTHERS THEN
        err_code := SQLCODE;
        err_msg := substr(SQLERRM, 1, 500);
        INSERT INTO ERROR_LOG VALUES (sysdate, err_code, err_msg);      

END;

/
